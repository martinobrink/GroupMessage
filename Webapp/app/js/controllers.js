'use strict';

/* Controllers */


function GenericViewCtrl($scope) {
}
GenericViewCtrl.$inject = ['$scope'];


function ContactViewCtrl($scope, $http) {

    $scope.lastForm = {};

    $scope.sendForm = function(form) {
        $scope.lastForm = angular.copy(form);
        $http({
            method: 'POST',
            url: "/backend/email.php",
            data: {
                'contactname':$scope.form.name,
                'weburl':$scope.form.website,
                'email':$scope.form.email,
                'app':$scope.form.project,
                'subject':$scope.form.subject,
                'message':$scope.form.message
            },
            headers: {'Content-Type': 'application/x-www-form-urlencoded'}
        }).success(function(data, status, headers, config) {
                $scope.resultData = data;
                alert("Message sent successfully. We'll get in touch with you soon.");

            }).error(function(data, status, headers, config) {
                $scope.resultData = data;
                alert("Sending message failed.");
            });
    }

    $scope.resetForm = function() {
        $scope.form = angular.copy($scope.lastForm);
    }

}

HomeViewCtrl.$inject = ['$scope', '$http'];

function HomeViewCtrl($scope, $http) {

    $scope.lastForm = {};

    $scope.sendMessage = function(form) {
        var messageId = "1234"; //TODO get from server
        var url = "http://localhost:8282/groupmessage/message/"+messageId;
        var jsonToPut = "{'MessageId':'"+messageId+"', 'Text': '" + $scope.message + "'}";

        var client = new XMLHttpRequest();

        client.open("PUT", url, false);

        client.setRequestHeader("Content-Type", "application/json");

        client.send(jsonToPut);

        if (client.status == 200)
            alert("The request succeeded!\n\nThe response representation was:\n\n" + client.responseText)
        else
            alert("The request did not succeed!\n\nThe response status was: " + client.status + " " + client.statusText + ".");
    }

}

