
var EARTH_RADIUS = 6378137.0; //单位M 
var PI = Math.PI;

function getRad(d) {
    return d * PI / 180.0;
}

/** 
* approx distance between two points on earth ellipsoid 
* @param {Object} lat1 
* @param {Object} lng1 
* @param {Object} lat2 
* @param {Object} lng2 
*/
function getFlatternDistance(lat1, lng1, lat2, lng2) {
    var f = getRad((lat1 + lat2) / 2);
    var g = getRad((lat1 - lat2) / 2);
    var l = getRad((lng1 - lng2) / 2);

    var sg = Math.sin(g);
    var sl = Math.sin(l);
    var sf = Math.sin(f);

    var s, c, w, r, d, h1, h2;
    var a = EARTH_RADIUS;
    var fl = 1 / 298.257;

    sg = sg * sg;
    sl = sl * sl;
    sf = sf * sf;

    s = sg * (1 - sl) + (1 - sf) * sl;
    c = (1 - sg) * (1 - sl) + sf * sl;

    w = Math.atan(Math.sqrt(s / c));
    r = Math.sqrt(s * c) / w;
    d = 2 * w * a;
    h1 = (3 * r - 1) / 2 / c;
    h2 = (3 * r + 1) / 2 / s;

    return d * (1 + fl * (h1 * sf * (1 - sg) - h2 * (1 - sf) * sg));
}

function createMarker(map, latlng, label, html, color) {
    // alert("createMarker("+latlng+","+label+","+html+","+color+")");
    var contentString = '<b>' + label + '</b><br>' + html;
    var marker = new google.maps.Marker({
        position: latlng,
        map: map,
        shadow: iconShadow,
        //icon: getMarkerImage(color),
        shape: iconShape,
        title: label,
        zIndex: Math.round(latlng.lat() * -100000) << 5
    });
    marker.myname = label;

    google.maps.event.addListener(marker, 'click', function () {
        infowindow.setContent(contentString);
        infowindow.open(map, marker);
    });
    return marker;
}
function getMarkerImage(iconStr) {
    if ((typeof (iconStr) == "undefined") || (iconStr == null)) {
        iconStr = "red";
    }
    if (!icons[iconStr]) {
        icons[iconStr] = new google.maps.MarkerImage("http://www.google.com/mapfiles/marker" + iconStr + ".png",
        // This marker is 20 pixels wide by 34 pixels tall.
        new google.maps.Size(20, 34),
        // The origin for this image is 0,0.
        new google.maps.Point(0, 0),
        // The anchor for this image is at 6,20.
        new google.maps.Point(9, 34));
    }
    return icons[iconStr];

}

function fitBounds(arrObj) {
    var bounds = new window.google.maps.LatLngBounds();

    // extend bounds for each record
    jQuery.each(arrObj, function (key, val) {
        var myLatlng = new window.google.maps.LatLng(val.lat(), val.lng());
        bounds.extend(myLatlng);
    });
    map.fitBounds(bounds);
}

var infowindow = new google.maps.InfoWindow(
  {
      size: new google.maps.Size(150, 50)
  });

var icons = new Array();
icons["red"] = new google.maps.MarkerImage("mapIcons/marker_red.png",
      // This marker is 20 pixels wide by 34 pixels tall.
      new google.maps.Size(20, 34),
      // The origin for this image is 0,0.
      new google.maps.Point(0, 0),
      // The anchor for this image is at 9,34.
      new google.maps.Point(9, 34));

// Marker sizes are expressed as a Size of X,Y
// where the origin of the image (0,0) is located
// in the top left of the image.

// Origins, anchor positions and coordinates of the marker
// increase in the X direction to the right and in
// the Y direction down.
var iconImage = new google.maps.MarkerImage('mapIcons/marker_red.png',
    // This marker is 20 pixels wide by 34 pixels tall.
    new google.maps.Size(20, 34),
    // The origin for this image is 0,0.
    new google.maps.Point(0, 0),
    // The anchor for this image is at 9,34.
    new google.maps.Point(9, 34));
var iconShadow = new google.maps.MarkerImage('http://www.google.com/mapfiles/shadow50.png',
    // The shadow image is larger in the horizontal dimension
    // while the position and offset are the same as for the main image.
    new google.maps.Size(37, 34),
    new google.maps.Point(0, 0),
    new google.maps.Point(9, 34));
// Shapes define the clickable region of the icon.
// The type defines an HTML &lt;area&gt; element 'poly' which
// traces out a polygon as a series of X,Y points. The final
// coordinate closes the poly by connecting to the first
// coordinate.
var iconShape = {
    coord: [9, 0, 6, 1, 4, 2, 2, 4, 0, 8, 0, 12, 1, 14, 2, 16, 5, 19, 7, 23, 8, 26, 9, 30, 9, 34, 11, 34, 11, 30, 12, 26, 13, 24, 14, 21, 16, 18, 18, 16, 20, 12, 20, 8, 18, 4, 16, 2, 15, 1, 13, 0],
    type: 'poly'
};
