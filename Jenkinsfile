pipeline {
  agent {
    docker {
      image 'maven:3.3.3'
    }

  }
  stages {
    stage('Build') {
      parallel {
        stage('Build') {
          steps {
            sh 'mvn --version'
          }
        }
        stage('Build 2') {
          steps {
            error 'STOP!'
          }
        }
      }
    }
  }
}