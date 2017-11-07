'use strict';

angular.module('myApp.view2', ['ngRoute'])

.config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/view2', {
    templateUrl: 'view2/view2.html',
    controller: 'View2Ctrl'
  });
}])

.controller('View2Ctrl', function($scope, $http) {	 

		$scope.UpdateData = function(){
		var data = $.param({
		id:$scope.id,
		firstName:$scope.firstName,
		lastName: $scope.lastName,
		jobTitle:$scope.jobTitle	
	   });

			$http.put('http://localhost:3928/api/persons?'+data)
			.success(function (data,status,headers){
			$scope.ServerResponse = data;				
		})
	}
		

});
