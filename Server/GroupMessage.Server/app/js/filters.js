'use strict';

/* Filters */

angular.module('myApp.filters', [])
    .filter('deviceImage', function() {
        return function(user) {
            return user.DeviceToken ? '\u2713' : '\u2718';
        };
    })
    .filter('interpolate', ['version', function(version) {
        return function(text) {
            return String(text).replace(/\%VERSION\%/mg, version);
        };
    }]);
