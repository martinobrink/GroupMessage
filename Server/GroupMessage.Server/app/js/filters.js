'use strict';

/* Filters */

angular.module('myApp.filters', [])
    .filter('displayDate', function () {
        return function (dateString) {
            return eval(dateString.replace(/\/Date\((\d+)\)\//gi, "new Date($1).toLocaleString('da-DK')"));
        };
    })
    .filter('deviceImage', function () {
        return function (user) {
            if (user.DeviceToken) {
                if (user.DeviceOs == 1) {
                    return 'img/android.png';
                } else {
                    return 'img/ios.png';
                }
            } else {
                return 'img/notconnected.png';
            }
        };
    })
    .filter('interpolate', ['version', function(version) {
        return function(text) {
            return String(text).replace(/\%VERSION\%/mg, version);
        };
    }]);
