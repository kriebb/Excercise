# Implement a file reading "library"

- Our business wants us to write a file reading implementation and assigns a user story to us. 
This user story states that "A user should be able to read a text file". 
Please implement this a simplistic version of this in C# and record this version inside a Git repository, please tag this as version 1 at the end of the implementation. 

- Some time later our business asks us to extend the library and creates a new user story stating that "A user should be able to read an XML file". Extend your implementation so that this behaviour is possible, record the changes again in the Git repository and tag them as version 2 at the end of the implementation. 

- Again some time later our business asks us to change the library so that they are able to read encrypted TEXT files. 
  - They create a user story stating that "A user should be able to read an encrypted TEXT file". 
  - Our user story contains some more detail stating that the user will tell the system that it needs to read an encrypted TEXT file. 
  - Please note that the encryption algorithm is of no concern and can be for example a simple reverse of the text. 
  - Also note that we should be able to switch the encryption without actually changing the code. 
  - Implement your changes and record them again in the Git repository tagging them as *version 3* at the end of the implementation. 

- Later in time the business requires us to again change the library an extend it with role based security for XML files. 
    - A user story is assigned stating that "A user should be able to read XML files in role based security context". 
    - Some examples are stated such as "eg. admin can read everything, other roles can only read limited set of files" and us such we're not interested in the actual role based security system. 
    - As such you can provide a simplistic implementation, however make sure that the switch to a real role based security system should be possible without actually changing the code! 
    - Implement your changes and record them again in the Git repository tagging them as *version 4* at the end of the implementation. 

- At some point in time the business asks us to enable the encrypted reading feature also for XML files. 
  - Implement your changes and record them again in the Git repository tagging them as *version 5* at the end of the implementation, 

- Even some more time later the business asks us to enable the role based security reading feature also for TEXT files. 
Implement your changes and record them again in the Git repository tagging them as *version 6* at the end of the implementation. 

- As a last change the business asks us to also add the ability to read JSON files. 
They create 3 user stories: 
  - "A user should be able to read JSON files" 
  - "Auser should be able to read encrypted JSON files" 
  - "A user should be able to read JSON files in role based security context" 
Implement your changes for each feature separatly and record them again in the Git repository tagging each version as a new *version 7, 8 & 9* at the end of the implementation of each feature. 

- BONUS: 
  - Implement a simple GUI/CLI application allowing to specify which file type you want to read, specify to use the encryption system and specify if role based security is needed or not. 
  - As such I can start the application, 
    - it will ask me 
      - the file type, 
      - to use the encryption system and 
      - if role based security should be used or not. 
        - If role based security is used it will ask my role and then we will read the file and show the output. 
  - After this I can read another file in another way without needing to restart the application. 
