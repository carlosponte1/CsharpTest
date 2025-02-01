pipeline {
    agent {
        label 'docker' // Define el agente como un nodo que soporta Docker
    }

    parameters {
        choice(
            name: 'ENVIRONMENT',
            choices: ['ppd', 'qa', 'dev'],
            description: 'Run test on (qa, ppd, or dev)'
        )
        choice(
            name: 'SCRIPT',
            choices: ['smoke', 'regression'],
            description: 'Which test suite to run? (e.g., smoke, regression, etc.)'
        )
        string(
            name: 'ADDITIONAL_PARAM',
            defaultValue: 'defaultValue',
            description: 'Example of a string parameter'
        )
        booleanParam(
            name: 'JIRA_TRACKING',
            defaultValue: false,
            description: 'Enable or disable test execution tracking in Jira'
        )
    }

    environment {
        CI = "true"
        IMAGE_NAME = "dotnet-integration-tests"
    }

    options {
        disableConcurrentBuilds()
        skipStagesAfterUnstable()
        timeout(time: 540, unit: 'MINUTES')
        parallelsAlwaysFailFast()
    }

    stages {

        stage('Build Docker Image') {
            steps {
                script {
                    sh "docker build -f Dockerfile -t ${IMAGE_NAME} ."
                }
            }
        }

        stage('Run Integration Tests') {
            steps {
                script {
                    withCredentials([usernamePassword(
                        credentialsId: 'svc.qa.auto.sb',
                        usernameVariable: 'USERNAME',
                        passwordVariable: 'PASSWORD'
                    )]) {
                        sh """
                        docker run --rm -i ${IMAGE_NAME} sh -c '
                        cd /src;
                        echo "Running Integration Tests...";
                        dotnet test CsharpPlaywith.sln --configuration Release
                        '
                        """
                    }
                }
            }
        }
    }

    post {
        always {
            echo 'Done...'
            cleanWs()
        }
    }
}
