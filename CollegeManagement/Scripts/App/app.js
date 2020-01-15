(function () {

    var collegeApp = angular.module('collegeApp', ['Courses', 'Teachers', 'Subjects', 'Students', 'ngMaterial', 'ngRoute', 'ngMessages']);

    collegeApp.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {  
      
        $routeProvider.when("/home", {
            //template: "<h2>Hello</h2>"   //Render custom design  
            templateUrl: "/Home/Courses"
        })
        //.when("/editcourse", {
        //    templateUrl: "/Home/EditCourse"
        //})
        .when("/teachers", {
            templateUrl: "/Home/Teachers"
        })
        .when("/subjects", {
            templateUrl: "/Home/Subjects"
        })
        .when("/students", {
            templateUrl: "/Home/Students"
        })
        .otherwise({
            redirectTo: "/home"
        });
  
        $locationProvider.hashPrefix('');  
    }]);

})();