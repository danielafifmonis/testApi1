'use strict';

angular.module('myApp.view1', ['ngRoute'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/view1', {
    templateUrl: 'view1/view1.html',
    controller: 'View1Ctrl'
  });
}])


.controller('View1Ctrl', function($scope, $http) {
	 $http.get("http://localhost:3928/api/persons").success(function (data){
			$scope.people = data;
			console.log(data);
		});

});