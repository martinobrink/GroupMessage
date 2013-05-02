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

MessagesViewCtrl.$inject = ['$scope', '$http'];

function MessagesViewCtrl($scope, $http) {

    $scope.lastForm = {};

    $scope.sendMessage = function(form) {
        var messageId = "1234"; //TODO get from server
        var url = "/groupmessage/message/"+messageId;
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

UsersViewCtrl.$inject = ['$scope', '$http'];

function UsersViewCtrl($scope, $http) {

    $scope.lastForm = {};

    $scope.init = function () {
        var url = "/groupmessage/user/";

        var client = new XMLHttpRequest();

        client.open("GET", url, false);

        client.setRequestHeader("Content-Type", "application/json");

        client.send();

        if (client.status == 200) {
            $scope.users=JSON.parse(client.responseText);
        }
        else
            alert("The request did not succeed!\n\nThe response status was: " + client.status + " " + client.statusText + ".");
    };

    $scope.deleteUser = function(phoneNumber) {
        var url = "/groupmessage/user/"+phoneNumber;

        var client = new XMLHttpRequest();

        client.onreadystatechange = function() {
            if (client.readyState != 4)  { return; }
            $scope.init()
        };

        client.open("DELETE", url, false);

        client.send();
    }

}