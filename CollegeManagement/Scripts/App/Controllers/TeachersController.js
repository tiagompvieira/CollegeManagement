(function () {

    var customers = angular.module('Teachers', ['ngRoute', 'ngMaterial', 'ngMessages']);

    //angular.module('CollegeApp', []).controller('CustomerController', function ($scope, $http, $location, $window) {
    customers.controller('TeachersController', function ($scope, $http, $location, $window, $mdDialog) {

        var minDate = new Date();
        var maxDate = new Date();

        minDate.setFullYear(minDate.getFullYear() - 90);
        maxDate.setFullYear(maxDate.getFullYear() - 21);

        $scope.teacher = {
            Name: '',
            Id: null,
            Salary: 635,
            DateOfBirth: maxDate
        };

        $scope.message = null;
        $scope.errorMessage = null;
        $scope.teachers = [];
        $scope.result = "color-default";
        $scope.isViewLoading = false;
        $scope.isEdit = false;

        //get called when user submits the form
        $scope.reload = function () {
            $scope.isViewLoading = true;
            $scope.errors = [];
            //$http service that send or receive data from the remote server
            $http(
            {
                method: 'GET',
                url: 'api/Teachers/GetTeachersSummary',
                data: { }
            }).then(function successCallback(response) {

                if (response.data.Success === true) {
                    $scope.teachers = response.data.Data;
                }
                else {
                    $scope.errorMessage = 'Unexpected Error while obtaining data!';
                }

            }, function errorCallback(response) {
                $scope.errors = [];
                $scope.errorMessage = 'Unexpected Error while obtaining data!';
            });

            $scope.isViewLoading = false;
        };

        $scope.edit = function (id) {

            $scope.teacher = {
                Name: '',
                Id: null,
                Salary: null,
                DateOfBirth: null
            };

            if (id) {
                $http(
                {
                    method: 'GET',
                    url: 'api/Teachers/GetTeacher/' + id,
                    data: { }
                }).then(function successCallback(response) {

                    if (response.data.Success === true) {
                        $scope.teacher = response.data.Data;
                        $scope.isEdit = true;
                    }
                    else {
                        $scope.errorMessage = 'Unexpected Error while obtaining data!';
                    }

                }, function errorCallback(response) {
                    $scope.errors = [];
                    $scope.errorMessage = 'Unexpected Error while obtaining data!';
                });
            }
            else {
                $scope.isEdit = true;
            }
        }

        $scope.save = function () {

            if ($scope.teacher.Name != undefined && $scope.teacher.Name.trim().length == 0) {
                $scope.errorMessage = 'Invalid data!';
                return;
            }

            $http(
              {
                  method: 'POST',
                  url: 'api/Teachers/SaveTeacher/',
                  data: $scope.teacher
              }).then(function successCallback(response) {

                  if (response.data.Success === true) {
                      $scope.reload();
                      $scope.isEdit = false;
                  }
                  else {
                      $scope.errorMessage = 'Error saving data!';
                  }

              }, function errorCallback(response) {
                  $scope.errors = [];
                  $scope.errorMessage = 'Error saving data!';
              });
        }

        $scope.delete = function (ev, id) {

            var confirm = $mdDialog.confirm()
            .title('Would you like to delete this teacher?')
            .ariaLabel('Teacher Deletion')
            .targetEvent(ev)
            .ok('Delete')
            .cancel('Cancel');

            $mdDialog.show(confirm).then(function () {

                $scope.isViewLoading = true;

                $http(
                  {
                      method: 'POST',
                      url: 'api/Teachers/DeleteTeacher/' + id,
                      data: $scope.course
                  }).then(function successCallback(response) {

                      if (response.data.Success === true) {
                          $scope.reload();
                      }
                      else {
                          $scope.errorMessage = 'Error deleting data!';
                      }

                      $scope.isViewLoading = false;

                  }, function errorCallback(response) {
                      $scope.errors = [];
                      $scope.errorMessage = 'Error deleting data!';
                      $scope.isViewLoading = false;
                  });

            });
        }

        $scope.cancel = function () {
            $scope.isEdit = false;
        }

        $scope.reload();

    }).config(function ($locationProvider) {
        //default = 'false'
        $locationProvider.html5Mode(true);
    });

})();