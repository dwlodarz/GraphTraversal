'use strict';

var myApp = angular.module('app.controllers', ['ngRoute', 'ui.bootstrap', 'ngTouch', 'ngAnimate','app.services', 'app.controllers']);

myApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when(
    	'/',
    	{
    	    templateUrl: 'app/main/main.html',
    	    controller: 'MainController'
    	});
    $routeProvider.otherwise(
        {
            redirectTo: '/view1'
        });
}]);

