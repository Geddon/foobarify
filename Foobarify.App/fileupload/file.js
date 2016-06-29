'use strict';

angular.module('myApp.fileupload', ['ngRoute'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/fileupload', {
    templateUrl: 'fileupload/fileupload.html',
    controller: 'FileuploadCtrl'
  });
}])

.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function(scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;
            
            element.bind('change', function(){
                scope.$apply(function(){
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}])


.controller('FileuploadCtrl', ['$scope', '$http', '$sce', function($scope, $http, $sce) {

      $scope.foobarText = '';
      $scope.fileForm = {};
      $scope.errorMsg = '';

      $scope.trustAsHtml = function(string) {
          return $sce.trustAsHtml(string);
      };

      $scope.submitForm = function(){
        $scope.errorMsg = '';

        var uploadUrl = 'http://localhost:58241/api/files';

        var fd = new FormData();
        fd.append('file', $scope.file);
        $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: {'Content-Type': undefined}
        })
        .success(function(data){
          $scope.foobarText = data;
        })
        .error(function(data){
          $scope.errorMsg = data.ExceptionMessage;
        });
    };

}]);