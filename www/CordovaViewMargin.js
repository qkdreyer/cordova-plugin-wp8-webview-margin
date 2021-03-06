﻿
var exec = require("cordova/exec");
var noop = function () {};

function CordovaViewMargin()
{
    this.left = 0;
    this.top = 0;
    this.right = 0;
    this.bottom = 0;
}

CordovaViewMargin.prototype.setCordovaViewMargin = function(successCallback, errorCallback, margins) {
    if (!margins) margins = {};
    var left = margins.left || this.left;
    var top = margins.top || this.top;
    var right = margins.right || this.right;
    var bottom = margins.bottom || this.bottom;

    return exec(successCallback || noop, errorCallback || noop, "CordovaViewMargin", "setMargin", [left, top, right, bottom]);
};

CordovaViewMargin.prototype.fixCordovaViewMargin = function(successCallback, errorCallback) {
    var diffPixel = document.documentElement.clientHeight - window.innerHeight;
    var ratioDPI = screen.deviceYDPI / screen.logicalYDPI;
    var offset = - Math.round(diffPixel * ratioDPI);

    if (isNaN(offset) || offset === this.top) {
        return false;
    }

    return this.setCordovaViewMargin(successCallback, errorCallback, {top: offset});
};

document.addEventListener("deviceready", function() {
    window.CordovaViewMargin.fixCordovaViewMargin();
}, false);

cordova.addConstructor(function() {
    window.CordovaViewMargin = new CordovaViewMargin();
    return window.CordovaViewMargin;
});
