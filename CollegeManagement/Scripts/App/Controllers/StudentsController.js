(function () {

    var customers = angular.module('Students', ['ngRoute', 'ngMaterial', 'ngMessages']);

    //angular.module('CollegeApp', []).controller('CustomerController', function ($scope, $http, $location, $window) {
    customers.controller('StudentsController', function ($scope, $http, $location, $window, $mdDialog) {

        $scope.student = {
            Name: '',
            RegistrationNumber: '',
            CourseId: '',
            Subjects: []
        };

        $scope.message = null;
        $scope.errorMessage = null;
        $scope.students = [];
        $scope.studentEvaluation = {
            Data: [],
            Id: null
        };
        $scope.courses = [];
        $scope.subjects = [];
        $scope.result = "color-default";
        $scope.isViewLoading = false;
        $scope.isEdit = false;
        $scope.isEditSubjects = false;

        //get called when user submits the form
        $scope.reload = function () {
            $scope.isViewLoading = true;
            $scope.errors = [];
            //$http service that send or receive data from the remote server
            $http(
            {
                method: 'GET',
                url: 'api/Students/GetStudentsSummary',
                data: {}
            }).then(function successCallback(response) {

                if (response.data.Success === true) {
                    $scope.students = response.data.Data;
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

        $scope.getCourses = function () {

            $http(
            {
                method: 'GET',
                url: 'api/Courses/GetCourses',
                data: {}
            }).then(function successCallback(response) {

                if (response.data.Success === true) {
                    $scope.courses = response.data.Data;
                }

            }, function errorCallback(response) {
            });
        };

        $scope.getSubjects = function (id) {

            let studentId = "";

            if($scope.student.RegistrationNumber)
            {
                studentId = "/" + $scope.student.RegistrationNumber;
            }

            $http(
            {
                method: 'GET',
                url: 'api/Subjects/GetStudentSubjects/' + id + studentId,
                data: {}
            }).then(function successCallback(response) {

                if (response.data.Success === true) {
                    $scope.student.Subjects = response.data.Data;
                    $scope.student.CourseId = id;
                }

            }, function errorCallback(response) {
            });
        };

        $scope.getStudentEvaluation = function (id) {

            $scope.studentEvaluation.Id = id;

            $http(
           {
               method: 'GET',
               url: 'api/Students/GetStudentEvaluation/' + id,
               data: {}
           }).then(function successCallback(response) {

               if (response.data.Success === true) {
                   $scope.studentEvaluation.Data = response.data.Data;
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

        }

        $scope.edit = function (id) {

            $scope.student = {
                Name: '',
                RegistrationNumber: '',
                CourseId: '',
                Subjects: []
            };

            if (id) {
                $http(
                {
                    method: 'GET',
                    url: 'api/Students/GetStudent/' + id,
                    data: {}
                }).then(function successCallback(response) {

                    if (response.data.Success === true) {
                        $scope.student = response.data.Data;
                        let temp = $scope.student.CourseId;
                        $scope.student.CourseId = null;
                        $scope.student.CourseId = temp;

                        $scope.getSubjects($scope.student.CourseId);
                        //if ($scope.student.CourseId) {
                        //    $("#studentCourseId").trigger('onchange');
                        //}

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

            if ($scope.student.Name != undefined && $scope.student.Name.trim().length == 0) {
                $scope.errorMessage = 'Invalid data!';
                return;
            }

            $http(
              {
                  method: 'POST',
                  url: 'api/Students/SaveStudent/',
                  data: $scope.student
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

        $scope.saveEvaluation = function () {

            $scope.message = null;
            $scope.errorMessage = null;

            $http(
              {
                  method: 'POST',
                  url: 'api/Students/SaveStudentEvaluation',
                  data: $scope.studentEvaluation
              }).then(function successCallback(response) {

                  if (response.data.Success === true) {
                      $scope.reload();
                      $scope.isEditSubjects = false;
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
                .title('Would you like to delete this student?')
                .ariaLabel('Student Deletion')
                .targetEvent(ev)
                .ok('Delete')
                .cancel('Cancel');

            $mdDialog.show(confirm).then(function () {

                $scope.isViewLoading = true;

                $http(
                  {
                      method: 'POST',
                      url: 'api/Students/DeleteStudent/' + id,
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

        $scope.toggle = function (item) {
            var idx = $scope.student.Subjects.indexOf(item);
            $scope.student.Subjects[idx].Selected = !item.Selected;
        };

        $scope.cancel = function () {
            $scope.isEdit = false;
            $scope.isEditSubjects = false;
        }

        $scope.editSubjects = function (studentId) {
            $scope.isEditSubjects = true;

            $scope.getStudentEvaluation(studentId);
        }

        $scope.reload();
        $scope.getCourses();

    }).config(function ($locationProvider) {
        //default = 'false'
        $locationProvider.html5Mode(true);
    });

})();