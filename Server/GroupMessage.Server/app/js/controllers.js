'use strict';

/* Controllers */


function GenericViewCtrl($scope) {
}
GenericViewCtrl.$inject = ['$scope'];

MessagesViewCtrl.$inject = ['$scope', '$http'];

function MessagesViewCtrl($scope, $http) {

    $scope.lastForm = {};

    $scope.sendMessage = function(textMessage) {
        var messageId = "messageId"+new Date().getTime();
        var url = "/groupmessage/message/"+messageId;
        var message = {MessageId:messageId, Text:textMessage};

        $http.put(url, message);
    }

}

UsersViewCtrl.$inject = ['$scope', '$http'];

function UsersViewCtrl($scope, $http) {

    $scope.lastForm = {};

    $scope.init = function () {
        $http.get("/groupmessage/user/").then(function(result){
            var data = result.data;
            $scope.users = data;
        })
    };

    $scope.deleteUser = function(phoneNumber) {
        $http.delete("/groupmessage/user/"+phoneNumber).then(function(){
            $scope.init();
        })
    }

    $scope.update = function(user) {
        $http.put("/groupmessage/user/", JSON.stringify($scope.user)).then(function(){
            $scope.user = null;
            $scope.init();
        })
    }

    $scope.editUser = function(user) {
        $scope.user = user;
    }
}