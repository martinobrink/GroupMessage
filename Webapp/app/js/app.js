'use strict';


// Declare app level module which depends on filters, and services
angular.module('myApp', ['myApp.filters', 'myApp.services', 'myApp.directives']).
  config(['$routeProvider', function($routeProvider) {
    $routeProvider.when('/home', {templateUrl: 'partials/home.html', controller: GenericViewCtrl});
    $routeProvider.when('/messages', {templateUrl: 'partials/messages.html', controller: MessagesViewCtrl});
    $routeProvider.when('/users', {templateUrl: 'partials/users.html', controller: GenericViewCtrl});
    $routeProvider.when('/contact', {templateUrl: 'partials/contact.html', controller: ContactViewCtrl});
    $routeProvider.when('/imprint', {templateUrl: 'partials/imprint.html', controller: GenericViewCtrl});
    $routeProvider.otherwise({redirectTo: '/home'});
  }]);
