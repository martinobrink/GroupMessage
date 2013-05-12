'use strict';

/* Filters */

angular.module('myApp.filters', [])
    .filter('displayDate', function () {
        return function (dateString) {
            var date = new Date(dateString);
            var year = date.getFullYear();
            var month = date.getMonth() + 1;//seems like a weird bug in ecmascript datetime handling??
            var day = date.getDate();
            var hours = ('0' + date.getHours()).slice(-2);//'0'+slice: zero-pad if necessary
            var minutes = ('0' + date.getMinutes()).slice(-2);//'0'+slice: zero-pad if necessary
            var seconds = ('0' + date.getSeconds()).slice(-2);//'0'+slice: zero-pad if necessary
            return day + '/' + month + '-' + year + ' ' + hours + ':' + minutes + ':' + seconds;
        };
    })
    .filter('deviceImage', function () {
        return function (user) {
            if (user.DeviceToken) {
                if (user.DeviceOs == 'Android') {
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
