var ddlText, ddlValue, ddl, lblMesg, TypeDD, MSNBx;

function CacheItems() {

    ddlText = new Array();

    ddlValue = new Array();

    ddl = document.getElementById(MSNFilesDD);

    lblMesg = document.getElementById(lblMessage);

    for (var i = 0; i < ddl.options.length; i++) {

        ddlText[ddlText.length] = ddl.options[i].text;

        ddlValue[ddlValue.length] = ddl.options[i].value;

    }

}

function StartUpPortal()
    {

    CacheItems();
    ShowHideMSNFileList();
}

function ShowHideMSNFileList() {

    ddlb = document.getElementById(ddlItems);
    msbox = document.getElementById(MSNBx);
    AirSelect = document.getElementById(TypeDD);
    MDiv = document.getElementById(MsnDiv);
    FDiv = document.getElementById(FileDiv);
    MSNFDiv = document.getElementById(MSNFileDiv);
    MSNFDD = document.getElementById(MSNFilesDD);

    if (AirSelect && ddlb) {
        var txtval = AirSelect.options[AirSelect.selectedIndex].value;
        //txtval = txtval.split("|")[1];
        if (txtval == "Airbus") {
            ddlb.style.visibility = 'hidden';
            msbox.style.visibility = 'visible';
            FDiv.style.display = 'block';
            MDiv.style.display = 'none';
            MSNFDiv.style.display = 'block';
            MSNFDD.style.visibility = 'visible';
        }
        else {
            ddlb.style.visibility = 'visible';
            msbox.style.visibility = 'hidden';
            FDiv.style.display = 'none';
            MDiv.style.display = 'block';
            MSNFDiv.style.display = 'none';
            MSNFDD.style.visibility = 'hidden';
        
        }


    }

}

function FilterItems(value) {


    //alert(document.getElementById(lblMessage).textContent);


    ddl.options.length = 0;

    for (var i = 0; i < ddlText.length; i++) {

        if (ddlText[i].toLowerCase().indexOf(value) != -1) {

            AddItem(ddlText[i], ddlValue[i]);

        }

    }

    if (ddl.options.length == 1) {
        lblMesg.innerHTML = ddl.options.length + " file located";
    }
    else {

        lblMesg.innerHTML = ddl.options.length + " files located";
    }

    if (ddl.options.length == 0) {

        AddItem("No items found.", "");

    }

}



function AddItem(text, value) {

    var opt = document.createElement("option");

    opt.text = text;

    opt.value = value;

    ddl.options.add(opt);

}