pipeline {
  agent any
  stages {
    
        // Kompiluje aplikacje
    stage("Restore application") {
      steps {
        echo "Restoring the application..."
       sh 'dotnet restore'
      }
    }
  }
}

    /*
    // Kompiluje aplikacje
    stage("Build application") {
      steps {
        echo "Compiling the application..."
       sh 'dotnet build "HotelBooking/HotelBooking.csproj" -c Release'
      }
    }
    
        // Uruchamiam aplikacje
    stage("Run application") {
      steps {
        echo "Running the application..."
       sh 'dotnet run -p ./HotelBooking/HotelBooking.csproj'
      }
    }
  }
}
/*
    
    // Kompiluje test integracyjny
    stage("Build integration test") {
      steps {
        echo "Compiling the test application..."
       sh 'dotnet build "HotelBooking.xUnit.IntegrationTest/HotelBooking.xUnit.IntegrationTest.csproj" -c Release'
      }
    }    	
    
        // Publikuje aplikacje
    stage("Publish application") {
      steps {
        echo "Publishing application..."
        sh 'dotnet publish "HotelBooking/HotelBooking.csproj" -c Release -o publish'
      }
    }
    
    // Buduje obraz Dockera dla Docker Registery 
    stage("Build Docker image for DockerHub"){
      steps{
        echo "Building Docker image for Docker Registery..."
        // lfarul to mój username na dockerhub i musi być w nazwie image / nazwa obrazu : wersja obrazu
        sh 'docker build -t lfarul/bookinghotel:1.0 .'
      }
    }
    
     // Buduje obraz Dockera dla Google Cloud
    stage("Build Docker image for GoogleCloud"){
      steps{
        echo "Building Docker image for Google Repository..."
        sh 'docker build -t gcr.io/nowyprojekt-235718/bookinghotel:1.0 .'
      }
    }  
    
    // Robie push obrazu Dockera na chmure Dockera
    stage("Push Docker image to Docker Registery"){
      steps{
        echo "Pushing Docker image to Docker Registery..."
        withCredentials([string(credentialsId: 'docker-pwd', variable: 'dockerHubpwd')]) {
          sh "docker login -u lfarul -p ${dockerHubpwd}"
        }
        sh 'docker push lfarul/bookinghotel:1.0'
      }
    }
    
    // Uruchamiam aplikacje w kontenerze Dockera na zmapowanym porcie 8282
    stage("Run Docker container"){
      steps{
        echo "Running Docker container..."
        sh 'docker run -d -p 9000:5000 lfarul/bookinghotel:1.0'
      }
    }
  }
}

/*

    // Robie push obrazu Dockera na chmure Google
    //stage("Push Docker image to Google Cloud"){
      //steps{
        //echo "Pushing Docker image to Google Cloud..."
        //withCredentials([string(credentialsId: 'docker-pwd', variable: 'dockerHubpwd')]) {
          //sh "docker login -u lfarul -p ${dockerHubpwd}"
        //}
        //sh 'docker push gcr.io/nowyprojekt-235718/tm2:3.0'
      //}
    //}
  }
}
*/
