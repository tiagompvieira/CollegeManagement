(function () {

    var customers = angular.module('Subjects', ['ngRoute', 'ngMaterial', 'ngMessages']);

    //angular.module('CollegeApp', []).controller('CustomerController', function ($scope, $http, $location, $window) {
    customers.controller('SubjectsController', function ($scope, $http, $location, $window, $mdDialog) {

        var minDate = new Date();
        var maxDate = new Date();

        minDate.setFullYear(minDate.getFullYear() - 90);
        maxDate.setFullYear(maxDate.getFullYear() - 21);

        $scope.subject = {
            Name: '',
            Id: null,
            Salary: 635,
            DateOfBirth: maxDate
        };

        $scope.teachers = [];
        $scope.message = null;
        $scope.errorMessage = null;
        $scope.subjects = [];
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
                url: 'api/Subjects/GetSubjectsSummary',
                data: {}
            }).then(function successCallback(response) {

                if (response.data.Success === true) {
                    $scope.subjects = response.data.Data;
                }
                else {
                    $scope.errorMessage = 'Unexpected Error while obtaining data!';
                }

                $scope.isViewLoading = false;

            }, function errorCallback(response) {
                $scope.errors = [];
                $scope.errorMessage = 'Unexpected Error while obtaining data!';
                $scope.isViewLoading = false;
            });
        };

        $scope.getTeachers = function () {

           $http(
           {
               method: 'GET',
               url: 'api/Teachers/GetTeachers',
               data: {}
           }).then(function successCallback(response) {

               if (response.data.Success === true) {
                   $scope.teachers = response.data.Data;
               }
               else {
                   $scope.teachers = [];
               }

           }, function errorCallback(response) {
               $scope.teachers = [];
           });

        }

        $scope.edit = function (id) {

            $scope.subject = {
                Name: '',
                Id: null,
                Salary: null,
                DateOfBirth: null
            };

            if (id) {
                $http(
                {
                    method: 'GET',
                    url: 'api/Subjects/GetSubject/' + id,
                    data: {}
                }).then(function successCallback(response) {

                    if (response.data.Success === true) {
                        $scope.subject = response.data.Data;
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

        $scope.editSubjects = function (id) {

        }

        $scope.save = function () {

            if ($scope.subject.Name != undefined && $scope.subject.Name.trim().length == 0) {
                $scope.errorMessage = 'Invalid data!';
                return;
            }

            $http(
              {
                  method: 'POST',
                  url: 'api/Subjects/SaveSubject/',
                  data: $scope.subject
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
           .title('Would you like to delete this subject?')
           .ariaLabel('Subject Deletion')
           .targetEvent(ev)
           .ok('Delete')
           .cancel('Cancel');

            $mdDialog.show(confirm).then(function () {

                $scope.isViewLoading = true;

                $http(
                  {
                      method: 'POST',
                      url: 'api/Subjects/DeleteSubject/' + id,
                      data: {}
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
        $scope.getTeachers();

    }).config(function ($locationProvider) {
        //default = 'false'
        $locationProvider.html5Mode(true);
    });

})();