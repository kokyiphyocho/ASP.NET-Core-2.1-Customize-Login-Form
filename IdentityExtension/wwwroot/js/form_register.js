var registerApp = angular.module('registerApp', []);

registerApp.config(function ($compileProvider) {
    $compileProvider.preAssignBindingsEnabled(true)
});

registerApp.controller('registerController', function ($scope) {
    var stepTemplate    = 'Step $STEP of 3';
    var maxStep         = 3;
    var registerForm    = $('div .form_register form');
    var stepInput       = $('div.form-group input[name="Input.Step"]');

    // Initialize the step.
    if ((!isNaN(stepInput.val())) && (Number(stepInput.val()) > 0)) $scope.step = Number(stepInput.val());
    else $scope.step = 1;

    // Prvent screen flickering.
    registerForm.closest('body').attr('fa-initialized', 'true');

    $scope.stepinfo = "";

    $scope.refreshValue = function () {
        $scope.stepinfo = stepTemplate.replace('$STEP', $scope.step);
    };

    $scope.nextStep = function () {        
        
        if (($scope.step < maxStep) && ($scope.validateStep()))
        {
            $scope.step = $scope.step + 1;
            $scope.refreshValue();
        }
    }

    $scope.previousStep = function () {
        if ($scope.step > 1) {
            $scope.step = $scope.step - 1;
            $scope.refreshValue();
        }
    }

    $scope.validateStep = function () {
        
        var elementList = registerForm.find('div.form-group[ng-show="step==' + $scope.step + '"] input[name]');
        var validator = registerForm.validate();
        var success = true;

        elementList.each(function () {
            if (!validator.element('div.form-group input[name="' + $(this).attr('name') + '"]'))
            {
                success = false;
                return (false);
            }
        });

        return (success);
    }

    $scope.refreshValue();    
});

