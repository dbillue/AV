var app = angular.module('simpleFormApp', []);

app.controller('simpleFormController', function($scope) {
	$scope.fName = "",
	$scope.lName = "",
	$scope.pass1 = "",
	$scope.pass2 = "",
	$scope.hosts = [{id:1, fName:'Alex', lName:'Trebek'},
					{id:2, fName:'Vanna', lName:'White'},
					{id:3, fName:'Pat', lName:'Sajack'},
					{id:4, fName:'Bob', lName:'Barker'}];
	$scope.hideform = true;
	$scope.edit = true;
	$scope.error = false;
	$scope.incomplete = false;
	
	$scope.editUser = function(id) {
		$scope.hideform = false;
		$scope.pass1 = '';
		$scope.pass2 = '';
		if(id == 'new') {
			$scope.edit = true;
			$scope.fName = '';
			$scope.lName = '';
		} else {
			$scope.edit = false;
			$scope.fName = $scope.hosts[id-1].fName;
			$scope.lName = $scope.hosts[id-1].lName;	
		}
	};
	
	// Use watch service.
	$scope.$watch('fName', function() {$scope.test();});
	$scope.$watch('lName', function() {$scope.test();});	
	$scope.$watch('pass1', function() {$scope.test();});
	$scope.$watch('pass2', function() {$scope.test();});
	
	$scope.test = function() {
		if($scope.pass1 !== $scope.pass2) {
			$scope.error = true;
		} else {
			$scope.error = false;
		}
		
		$scope.incomplete = false;
		if($scope.edit && (!$scope.fName.length || !$scope.lName.length || !$scope.pass1.length || !$scope.pass2.length)) {
			$scope.incomplete = true;
		}
	}
	
	$scope.showMsg = function() {
		alert('Form Submitted');
	}
	
	$scope.hideHostForm = function() {
		$scope.hideform = true;
	}
});