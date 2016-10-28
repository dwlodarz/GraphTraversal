'use strict';

myApp.controller("MainController", ['$scope', '$log', 'graphService', function ($scope, $log, movieService) {

    $scope.shortestPath = function ()
    {

    }

    var initAlchemyGraphDrawing = function ()
    {
        var config = {
            dataSource: 'Assets/test.html',

            linkDistance: function () { return 40; },

            nodeTypes: { "node_type": ["Maintainer", "Contributor"] },
            caption: function (node) { return node.caption; }
        };

        alchemy = new Alchemy(config)
    }

    initAlchemyGraphDrawing();

}]);