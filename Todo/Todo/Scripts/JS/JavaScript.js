let formcontainer = document.getElementById("taskform");
let tablecontainer = document.getElementById("tasktable");
let createbutton = document.getElementById("newtaskcreation");
let canclelationbutton = document.getElementById("newtaskcancle");
formcontainer.style.display = "none";
canclelationbutton.style.display = "none";

let OpenForm = () => {
    createbutton.style.display = "none";
    $("#taskform").slideToggle();
    formcontainer.style.display = "block";
    canclelationbutton.style.display = "block";
    tablecontainer.classList.remove("col-sm-12");
    tablecontainer.classList.add("col-sm-8");
    tablecontainer.classList.add("col-md-7");
    //$("#tasktable").classList.remove("col-sm-12");
    //$("#tasktable").classList.add("col-sm-8");

}
let CloseForm = () => {
    createbutton.style.display = "block";
    $("#taskform").slideToggle();
    formcontainer.style.display = "none";
    canclelationbutton.style.display = "none";
    tablecontainer.classList.remove("col-sm-8");
    tablecontainer.classList.remove("col-md-7");
    tablecontainer.classList.add("col-sm-12");
}
let PaginationitemI=(pageindex)=>{
    if(pageindex != null)
    {
        debugger
        var showdata =
        {
            pageindex: pageindex,
            pagesize:10
        }
        $(".tasklist").empty();
        $.ajax({
            url: "/Todo/Getpaginatiotabledata",
            Method: "POST",
            data: showdata,
            success: function (data) {
                data = JSON.parse(data);
                document.getElementById("tableitem").innerHTML =
                data.map(function (element) {
                    var update="Pending";
                    let { TodoID, TodoName, TodoDetails, TodoDateandTime, TodoUpdate } = element;
                    if(TodoUpdate==1)
                    {
                        update="Complete";
                    }
                    return `
 
                    <tr>
                    <td> <input class="form-check-input" type="checkbox" value= "${TodoID}" id="flexCheckDefault"></td>
                    <td>${TodoName}</td>
                    <td>${TodoDetails}</td>
                    <td>${TodoDateandTime}</td>
                    <td>${update}</td>
                   
                    <td>
                    <a href="/Todo/DeleteTodo/${TodoID} class = "btn btn-danger" </a>
                      
                    </td>
                    <td>
                        <button id="getsingledetails" onclick="Updatedata(${TodoID})" class="btn btn-success">Update</button>
                       
                    </td>
                    <td>
                        <button id="getsingledetails"  data-toggle="modal" data-target="#myModal" onclick="Getsingledata(${TodoID})" class="btn btn-info"> Single Details</button>
                    </td>
                </tr>
                  `;
                }).join("");
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
       }
       else {
        document.getElementById("tableitem").innerHTML =
        `
        <p class="errormsg">Could Not find the data</p>
      `;
       }
}
let Updatedata=(id)=>{
    if (id != null) {
        var showdata =
        {
            id: id
        }
        $.ajax({
            url: "/Todo/GetSingleTodo",
            Method: "POST",
            data: showdata,
            success: function (data) {
                console.log(data);
                data = JSON.parse(data);
                $("#hiddenvalue").val(data.TodoID);
                $("#todotitle").val(data.TodoName);
                $("#TodoDetails").val(data.TodoDetails);
                console.log(data.TodoDateandTime);
                document.getElementById('TodoDateandTimevalue').style.display = 'block';
                var tempD =data.TodoDateandTime.split("-");
                var mydata=tempD[1] + "-" + tempD[0] + "-" + tempD[2];
                $("#TodoDateandTimevalue").val(mydata);
                OpenForm();
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
    else {
        document.getElementById("errormsgdev").innerHTML =
        `
        <p class="errormsg">Could Not find the data</p>
      `;
    }
}
let Getsingledata=(id)=>{
    if (id != null) {
        var showdata =
        {
            id: id
        }
        $.ajax({
            url: "/Todo/GetSingleTodo",
            Method: "POST",
            data: showdata,
            success: function (data) {
                $(".modal").show();
                data = JSON.parse(data);
                document.getElementById("modal-body").innerHTML =
                `
                <div class="">
                <h6>Title: ${data.TodoName}</h6>
                <h6>Details:</h6>
                <p>${data.TodoDetails}</p>
                <h6>Date and Time:</h6>
                <p>${data.TodoDateandTime}</p>
            </div>
              `;
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
    }
    else {
        document.getElementById("modal-body").innerHTML =
        `
        <p class="errormsg">Could Not find the data</p>
      `;
    }
}
    let Searchdata =()=>{
        debugger
        let searchdata=$("#example-search-input").val();
       if (searchdata != null) {
        var showdata =
        {
            name: searchdata
        }
        $(".tasklist").empty();
        $.ajax({
            url: "/Todo/SearchTodo",
            Method: "POST",
            data: showdata,
            success: function (data) {
                data = JSON.parse(data);
                document.getElementById("tableitem").innerHTML =
                data.map(function (element) {
                    var update="Pending";
                    let { TodoID, TodoName, TodoDetails, TodoDateandTime, TodoUpdate } = element;
                    if(TodoUpdate==1)
                    {
                        update="Complete";
                    }
                    return `
 
                    <tr>
                    <td> <input class="form-check-input" type="checkbox" value= "${TodoID}" id="flexCheckDefault"></td>
                    <td>${TodoName}</td>
                    <td>${TodoDetails}</td>
                    <td>${TodoDateandTime}</td>
                    <td>${update}</td>
                   
                    <td>
                    <a href="/Todo/DeleteTodo/${TodoID} class = "btn btn-danger" </a>
                      
                    </td>
                    <td>
                        <button id="getsingledetails" onclick="Updatedata(${TodoID})" class="btn btn-success">Update</button>
                       
                    </td>
                    <td>
                        <button id="getsingledetails"  data-toggle="modal" data-target="#myModal" onclick="Getsingledata(${TodoID})" class="btn btn-info"> Single Details</button>
                    </td>
                </tr>
                  `;
                }).join("");
            },
            error: function () {
                alert('Failed to receive the Data');
                console.log('Failed ');
            }
        });
       }
       else {
        document.getElementById("tableitem").innerHTML =
        `
        <p class="errormsg">Could Not find the data</p>
      `;
       }
    }
 