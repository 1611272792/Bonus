//删除权限
function delete_Sure(RoleId)
{
    
    if (RoleId == "管理员")
    {
        $.toast("管理员权限不能删除", "cancel");
        return false;
    }
    $.confirm("确认删除该角色？", function ()
    {
        $.post("/Role/DelRole", { RoleId: RoleId }, function (data)
        {
            //dialog.toast(data, 'none', 1000);
            location.reload();
        })
    }, function ()
    {
        location.reload();
    });
}
//添加权限
$("#Add_Role").click(function ()
{
    
    $("#TianjiaRole").show();
    $("#RoleHidd").hide();
});

//搜索角色
$("#SearchRole").keyup(function ()
{
    var SearchRole = $("#SearchRole").val();
    console.log("SearchRole:" + SearchRole);
    $.ajax({
        type: "POST",
        url: '/Role/SelectRole',//@Url.Action("SelectNoticeByXq", "TeachBuidsMain")
        data: { RoleName: SearchRole },
        success: function (data)
        {
            $("#RoleHidd").html(data);
            $.hideLoading();
        }
        , beforeSend: function ()
        {
            $.showLoading("加载");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown)
        {
            //$.toast("搜索错误", "cancel");
            return false;
        }
    })
})

////弹出添加框
//$("#Add_Role").click(function ()
//{
//    $("#TianjiaRole").show();
//    $("#RoleHidd").hide();
//});




