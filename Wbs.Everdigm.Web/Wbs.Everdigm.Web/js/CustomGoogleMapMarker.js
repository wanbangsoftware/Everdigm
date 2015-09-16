function CustomMarker(latlng, map, args) {
    this.latlng = latlng;
    this.args = args;
    this.map = map;
    this.setMap(map);
    this.infoShown = false;
    this.infor = this.createWindow();
}

CustomMarker.prototype = new google.maps.OverlayView();

CustomMarker.prototype.draw = function () {

    var self = this;

    var div = this.div;

    if (!div) {

        div = this.div = document.createElement("div");

        div.className = "marker " + self.args.css;

        //div.style.position = 'absolute';
        //div.style.cursor = 'pointer';
        //div.style.width = '110px';
        //div.style.height = '20px';
        //div.style.opacity = 0.9;
        //div.style.background = '#F8C301';
        //div.style.textAlign = "center";
        //div.style.verticalAlign = "middle";
        //div.style.borderRadius = "4px";
        //div.style.padding = "3px";
        //div.style.color = "#FFFFFF";
        div.innerHTML = "<div class=\"arrow " + self.args.css + "\"></div><span>" + self.args.mac_id + "<span>";

        if (typeof (self.args.marker_id) !== "undefined") {
            div.dataset.marker_id = self.args.marker_id;
        }

        google.maps.event.addDomListener(div, "click", function (event) {
            self.showHideInfoWindow(!self.infoShown);
            google.maps.event.trigger(self, "click");
        });
        //google.maps.event.addDomListener(div, "mouseover", function (event) {
        //    self.showHideInfoWindow(true);
        //    google.maps.event.trigger(self, "mouseover");
        //});
        if (!self.info) {
            self.infor = self.createWindow();
        }
        google.maps.event.addDomListener(self.infor, "closeclick", function (event) {
            self.infoShown = false;
            google.maps.event.trigger(self, "mouseover");
        });
        var panes = this.getPanes();
        panes.overlayImage.appendChild(div);
    }

    var point = this.getProjection().fromLatLngToDivPixel(this.latlng);

    if (point) {
        div.style.left = (point.x - 55) + "px";
        div.style.top = (point.y - 30) + "px";
    }
};

CustomMarker.prototype.remove = function () {
    if (this.div) {
        if (this.div.parentNode) {
            this.div.parentNode.removeChild(this.div);
        }
        this.div = null;
    }
    // clear info window
    if (this.infor) {
        google.maps.event.clearInstanceListeners(this.infor);
        this.infor.close();
        this.infor = null;
    }
};

CustomMarker.prototype.getPosition = function () {
    return this.latlng;
};

CustomMarker.prototype.createWindow = function () {
    return new google.maps.InfoWindow({
        content: "<h4>" + this.args.mac_id + "</h4>You clicked marker of " + this.args.mac_id + "!"
    });
};

CustomMarker.prototype.getParams = function () { return this.args; };

CustomMarker.prototype.getDraggable = function () { return false; };

CustomMarker.prototype.showHideInfoWindow = function (shown) {
    if (!this.infor) { this.infor = this.createWindow(); }
    if (!this.infoShown) {
        this.infoShown = true;
        this.infor.open(this.map, this);
    } else {
        this.infoShown = false;
        this.infor.close();
    }
}