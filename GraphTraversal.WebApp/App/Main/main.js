'use strict';

myApp.controller("MainController", ['$scope', '$log', 'graphService', function ($scope, $log, graphService) {
    $scope.error = "";
    $scope.resetVisible = false;
    var getSelectedNodeIds = function () {
        $scope.error = "";

        var selectedNodes = _.filter(alchemy._nodes, function (o) { return o._state === "selected"; });
        if (selectedNodes.length != 2) {
            $scope.error = "No. of selected nodes should be 2."
        }

        return _.map(selectedNodes, 'id');
    }

    var markEdgesAsSelected = function (cocurrentNodeIds) {
        if (cocurrentNodeIds.length > 1) {
            for (var i = 0; i < cocurrentNodeIds.length - 1 ; i++) {
                var current = cocurrentNodeIds[i];
                var next = cocurrentNodeIds[i + 1];
                var edgeSelector = 'g#edge-' + current + '-' + next + '-0';
                $(edgeSelector).css('stroke', 'rgb(255, 0, 0)');
                $(edgeSelector).css('opacity', '1');
            }
        }
    }

    $scope.shortestPathLabel = "Shortest Path";
    $scope.shortestPath = function () {
        $scope.resetVisible = true;
        var selectedIds = getSelectedNodeIds();
        graphService.GetShortestPath(selectedIds[0], selectedIds[1])
            .then(function (data) {
                if (data != null && data.ShortestPathResult && data.ShortestPathResult.Path != null && data.ShortestPathResult.Path.length > 0) {
                    var concurrentIds = _.map(data.ShortestPathResult.Path, "id");
                    setTimeout(function(){markEdgesAsSelected(concurrentIds)}, 100);
                }
            })
            .catch(function (e) {
                $scope.error = "Unexpected error occurred."
            });
    }

    $scope.reset = function () {
        if (alchemy != null) {
            initAlchemyGraphDrawing();
            $scope.resetVisible = false;
            $scope.error = "";
        }
    }

    var initAlchemyGraphDrawing = function () {
        var config = {
            dataSource: 'http://localhost/GraphTraversal.WebServices/FrontEndService.svc/graphdata',
            linkDistance: function () { return 20; },
            caption: function (node) { return node.caption; },
        };

        alchemy = new Alchemy(config)
    }

    initAlchemyGraphDrawing();

}]);