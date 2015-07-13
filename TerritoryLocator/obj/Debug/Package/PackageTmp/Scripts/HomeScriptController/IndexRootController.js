TerritoryLocatorController.controller('IndexRootController',function ($scope, communicationService) {

    init();
    function init() {
        loadPresentationTemplate(IndexTemplateName, root, function (data) { $scope.$apply(function () { $scope.indexTemplate = data.TemplatePath; }); });
    };
});

