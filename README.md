# KServer library

Kesha's server library

### Beta 1.0

##### lightweight http server library

----

### Requirements:
* [Newtonsoft JSON](https://github.com/JamesNK/Newtonsoft.Json)


## Contents

- [Installation](#installation)
- [Quick start](#quick-start)
- [Send JSON](#Send-JSON)
- [Attributes](#Attributes)

## Installation
Just clone this project and use as project, or delete Program.cs file and use as library


## Quick start
    Controller controller = new Controller();
    
    action del = delegate(KClient client)
    {
        client.HTML("<h1>it works!</h1>");
    }
    controller.Get("/test",del);
    // will serve on localhost:3000
    controller.serve();

This was a base simple http server example.
To see the response for this example type in browser http://localhost:3000/test

----

## Send JSON
    action del = delegate(KClient client)
    {
        var dict = new Dictionary<string,int>();
        dict["tosts"]=5;
        client.Json(dict);
    }
or

    struct MyStruct
    {
        public int tosts;
    }
    action del = delegate(KClient client)
    {
        var mystruct = new MyStruct ();
        mystruct.tosts = 5;
        client.Json(mystruct);
    }


## Attributes
* client.RawBody -
returns raw body of client request
* client.GetHeader(/*header name*/ "Content-Type"); - returns value of header name
* controller.Post("/road", delegate); - make response for post request

 



