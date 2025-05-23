terraform { 
  cloud { 
    
    organization = "zinter-application" 

    workspaces { 
      name = "zinter-application" 
    } 
  } 
}