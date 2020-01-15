(function () {

    var customers = angular.module('Courses', ['ngRoute', 'ngMaterial', 'ngMessages']);

    //angular.module('CollegeApp', []).controller('CustomerController', function ($scope, $http, $location, $window) {
    customers.controller('CoursesController', function ($scope, $http, $location, $window, $mdDialog) {
        $scope.course = {
            Name: '',
            Id: null
        };

        $scope.message = '';
        $scope.courses = [];
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
                url: 'api/Courses/GetCoursesSummary',
                data: { }
            }).then(function successCallback(response) {

                if (response.data.Success === true) {
                    $scope.courses = response.data.Data;
                }
                else {
                    $scope.message = 'Unexpected Error while obtaining data!';
                }
                $scope.isViewLoading = false;

            }, function errorCallback(response) {
                $scope.errors = [];
                $scope.message = 'Unexpected Error while obtaining data!';
                $scope.isViewLoading = false;
            });
        };

        $scope.edit = function (id) {

            $scope.course = {
                Name: '',
                Id: null
            };

            if (id) {
                $http(
                {
                    method: 'GET',
                    url: 'api/Courses/GetCourse/' + id,
                    data: { }
                }).then(function successCallback(response) {

                    if (response.data.Success === true) {
                        $scope.course = response.data.Data;
                        $scope.isEdit = true;
                    }
                    else {
                        $scope.message = 'Unexpected Error while obtaining data!';
                    }

                }, function errorCallback(response) {
                    $scope.errors = [];
                    $scope.message = 'Unexpected Error while obtaining data!';
                });
            }
            else {
                $scope.isEdit = true;
            }
        }

        $scope.save = function () {

            if ($scope.course.Name != undefined && $scope.course.Name.trim().length == 0) {
                $scope.message = 'Invalid data!';
                return;
            }

            $http(
              {
                  method: 'POST',
                  url: 'api/Courses/SaveCourse/',
                  data: $scope.course
              }).then(function successCallback(response) {

                  if (response.data.Success === true) {
                      $scope.reload();
                      $scope.isEdit = false;
                  }
                  else {
                      $scope.message = 'Error saving data!';
                  }

              }, function errorCallback(response) {
                  $scope.errors = [];
                  $scope.message = 'Error saving data!';
              });
        }

        $scope.delete = function (ev, id) {

            var confirm = $mdDialog.confirm()
              .title('Would you like to delete this course and its subjects?')
              .ariaLabel('Courses Deletion')
              .targetEvent(ev)
              .ok('Delete')
              .cancel('Cancel');

            $mdDialog.show(confirm).then(function () {
               
                $scope.isViewLoading = true;

                $http(
                  {
                      method: 'POST',
                      url: 'api/Courses/deleteCourse/' + id,
                      data: $scope.course
                  }).then(function successCallback(response) {

                      if (response.data.Success === true) {
                          $scope.reload();
                      }
                      else {
                          $scope.message = 'Error deleting data!';
                      }

                      $scope.isViewLoading = false;

                  }, function errorCallback(response) {
                      $scope.errors = [];
                      $scope.message = 'Error deleting data!';
                      $scope.isViewLoading = false;
                  });

            }, function () {
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