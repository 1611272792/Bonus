

/*奖罚单选框单击事件*/
//$(".typeClick").click(function ()
//{
//    $(this).find("input[name=myprice]").prop("checked", true);
//});
/*选择金额单选框单击事件*/
$(".jexz").click(function ()
{
    //选中的金额
    var BonusMoney = $(this).find("input[type=hidden]").val();
    if (BonusMoney == "qt")
    {
        //选中的是其他金额
        document.getElementById("qtje").disabled = false;
        BonusMoney = $("#qtje").val();
        $("#xzfs").val("qt");
        $(".jexz").removeClass("ChooseMoney");
    }
    else
    {
        $(".jexz").removeClass("ChooseMoney");
        $(this).addClass("ChooseMoney");
        document.getElementById("qtje").disabled = true;
        $("#xzfs").val("a");
    }
});
//选择人
$("#addUsers").click(function ()
{
    alert("选中");
    $("#vv").hide();
    $("#ChooseEmp").show();
})

//原因
$("input:radio[name='res']").click(function ()
{
    var yuanyin = $(this).val();
    //alert(yuanyin);
    var aa = document.getElementById("TextArea1");//其他原因
    var bb = document.getElementById("Select1");//常用原因
    var cc = document.getElementById("btnAddCy");//添加常用原因按钮（只有在输入原因时才可以去添加为常用原因）
    //alert(bb.value);
    if (yuanyin == "0")
    {

        if (bb.value == "无常用原因")
        {
            bb.disabled = true;
        }
        else
        {
            bb.disabled = false;
        }
        aa.disabled = true;
        cc.disabled = true;
        //bb.disabled = false;
    }
    else
    {
        //其他
       
        if (bb.value == "无常用原因")
        {
            bb.disabled = true;
        }
        else
        {
            bb.disabled = true;
        }
        //bb.disabled = true;
        cc.disabled = false;
        aa.disabled = false;

    }
})
var $iosDialog1 = $('#iosDialog1');//确认
var $toast = $('#toast');//成功
var $toast_error = $("#toast_error");//错误
//添加常用原因
$("#btnAddCy").click(function ()
{
    
    
    $("#confirmMes").html("确定要将其添加为常用原因吗");
    $iosDialog1.fadeIn(200);
    $("#qd").unbind("click");
    $("#qd").click(function ()
    {
        $iosDialog1.fadeOut(100);
        //获取输入的原因
        var yy = $("#TextArea1").val().trim();
        if (yy == null || yy == "")
        {
            $("#errorMsg").html("原因不能为空");


            if ($toast_error.css('display') != 'none') return;

            $toast_error.fadeIn(100);
            setTimeout(function ()
            {
                $toast_error.fadeOut(100);
            }, 2000);
            return false;

        }
        var Userid = $("#benrenId").val().trim();
        var compid = $("#companyid").val().trim();
        $.post("/SendBonus/AddBonusReson", { CommReasons: yy, Userid: Userid, companyId: compid }, function (data)
        {
            if (data == "ok")
            {
                if ($toast.css('display') != 'none') return;

                $toast.fadeIn(100);
                setTimeout(function ()
                {
                    //重新加载常用原因
                   
                    $toast.fadeOut(100);

                }, 2000);
                $("#cyyy").load("/SendBonus/ReloadBonusReson?Userid=" + Userid + "&companyId=" + compid);
            }
            else
            {
                $("#errorMsg").html(data);
                if ($toast_error.css('display') != 'none') return;

                $toast_error.fadeIn(100);
                setTimeout(function ()
                {
                    $toast_error.fadeOut(100);
                }, 2000);
                return false;
            }
        });

    });

});
//$(".liclick").click(function ()
//{
//    $(this).find("input[name=momey]").prop("checked", true);
//    var BonusMoney = $("input:radio[name='momey']:checked").val();
//    //其他金额
//    if (BonusMoney == "ok")
//    {
//        document.getElementById("qtmoney").disabled = false;
//    }
//    else
//    {
//        document.getElementById("qtmoney").disabled = true;
//    }
//});

//自动补全
//$(function ()
//{
//    var CompanyId = $("#companyid").val();
    
//    var kw = $("#qq").val().trim().replace(/\s|\xA0/g, "");
   
//    var empName = "";
//    $("#qq").bigAutocomplete({
//        url: "/SendBonus/SelectUsersZidong?keyword=" + kw + "&CompanyId=" + CompanyId,
//        callback: function (data)
//        {
//            if (data != "" && data != null)
//            {
//                empName = data.title;
//                //alert(empName);
//                if (empName.length > 0)
//                {
//                    $("#emp").load("/SendBonus/SearchEmp?Name=" + empName + "&CompanyID=" + CompanyId)
//                }
//            }
//        }
//    });
//})


//确定(多选人)
$("#btnok").click(function ()
{
    
    //得到所有选中的复选框
    //var spCodesTemp = "";
    //$('input:checkbox[name=xzry]:checked').each(function (i)
    //{
    //    if (0 == i)
    //    {
    //        spCodesTemp = $(this).val();
    //    } else
    //    {
    //        spCodesTemp += ("," + $(this).val());
    //    }
    //});
    //alert("选择的："+spCodesTemp);
    //$("#ChooseEmp").hide();
    //$("#vv").show();
   
    ////alert("用户id:"+gh);
    //var info = spCodesTemp.split(",");

    //var arrat = new Array();
    ////找到下面的表格的工号
    //var tds = $("#luckyPer tr").find("td");
    //tds.each(function (index, elem)
    //{
    //    var td = $(this);
    //    arrat[index] = td.text();
    //    alert(arrat[index]);
    //});
    //var isok;
    //for (var i = 0; i < info.length; i++)
    //{
    //    var index = arrat.indexOf(info[i]);
    //    //alert(index);
    //    if (-index <= 0)
    //    {
    //        isok = -1;//代表存在
    //    }
    //}
    //alert("isok:" + isok);
    //if (isok == -1)
    //{
    //    alert("不能重复添加");
    //}
    //else
    //{
        
    //    var CompanyId = $("#companyid").val();
    //    var benrenId = $("#benrenId").val();
    //    $.post("/SendBonus/LuckPerson", { uid: spCodesTemp, companyId: CompanyId,BenrenID: benrenId }, function (data)
    //    {
           
    //        if (data.trim() == "" || data == null)
    //        {
               
    //            $("#errorMsg").html("人员添加有误，请仔细核对");


    //            if ($toast_error.css('display') != 'none') return;

    //            $toast_error.fadeIn(100);
    //            setTimeout(function ()
    //            {
    //                $toast_error.fadeOut(100);
    //            }, 2000);
    //            return false;
    //        }

    //        var $loadingToast = $('#loadingToast');
    //        $('#showLoadingToast').on('click', function ()
    //        {
    //            if ($loadingToast.css('display') != 'none') return;

    //            $loadingToast.fadeIn(100);
    //            setTimeout(function ()
    //            {
    //                $loadingToast.fadeOut(100);
    //            }, 2000);
    //        });
    //        //添加到表格中
    //        //alert("添加表格中");
    //        //$("#UserId").val("");
    //        $("#luckyPer").append(data);
    //        //document.getElementById("gongok").innerText = "确定";
    //        //$('#gongok').prop('disabled', false);
    //    })

    //}
    //alert(aa);
});
//搜索方法
function search()
{
    var CompanyId = $("#companyid").val();
    var kw = $("#userId").val().trim();;
    if (kw == "" || kw == null)
    {
        location.reload();
    }
    else
    {
        $("#emp").load("/SendBonus/SearchEmp?Name=" + kw + "&CompanyID=" + CompanyId)
    }
}

//删除添加错了的人
$("#luckyPer").on("click", ".delPerson", function ()
{
    $("#confirmMes").html("确定删除吗");
    $iosDialog1.fadeIn(200);
    
    var shanchu= $(this).parent().parent()
    $("#qd").click(function ()
    {
        shanchu.remove();
        $iosDialog1.fadeOut(100);
    });
    
    

});
$("#qx").click(function ()
{
    $iosDialog1.fadeOut(100);
});

var input = $('#userid').on('keyup', function ()
{
    console.log("11");
    var CompanyId = $("#companyid").val();

    var empName = $("#userid").val();
    console.log(empName);
    $("#emp").load("/SendBonus/SearchEmp?Name=" + empName + "&CompanyID=" + CompanyId)
});
var spCodesTemp2 = "";
var aa = $("#dd").val();


layui.use(['laydate', 'laypage', 'layer', 'table', 'carousel', 'upload', 'element'], function ()
{
    var element = layui.element;
    var layer = layui.layer;

    //监听折叠
    element.on('collapse(test)', function (data)
    {
        //layer.msg('展开状态：' + data.show);
    });

    /*其他金额失去焦点事件时判断是否大于200*/
    $("#qtmoney").blur(function ()
    {
        alert("11");
        var qtmoney = $("#qtmoney").val().trim();
        alert(qtmoney);
        //判断是否为数字
        if (isNaN(qtmoney))
        {
            layer.msg('请输入合法数字', { time: 3000, icon: 5 });
            return false;
        }
        else if(qtmoney>200)
        {
            layer.msg('最多不能超过200元', { time: 3000, icon: 5 });
            return false;
        }
    });

    var DisMan = $("#comeUserid").val();
    //自动补全
    $("#UserId").autocomplete({
        open: function (event, ui)
        {
           
            $('.ui-autocomplete').off('menufocus hover mouseover mouseenter');
        },
        autoFill: true,
        source: "/Trading/GetPersons?EmpId=" + DisMan,
        select: function (event, ui)
        {
            //alert("用户姓名：" + ui.item.label);
            var ghName=ui.item.label;
            var gh = ui.item.value;
            //alert("用户id:"+gh);
            var arrat = new Array();
            //找到下面的表格的工号
            var tds = $("#luckyPer tr").find("td");
            tds.each(function (index, elem)
            {
                var td = $(this);
                arrat[index] = td.text();
                //alert("aaa:"+arrat[index]);
            });
            var isok;
            var index2 = arrat.indexOf(gh);
            if (-index2 <= 0)
            {
                isok = -1;
            }
            else
            {
                isok = 0;
            }
            alert("isok:" + isok);
            //判断是否有重复添加
            if (isok == -1)
            {
                layer.msg('人员有重复，请仔细检查', { time: 3000, icon: 5 });
                return false;
            }
            
            //判断奖罚
            var types = $("input:radio[name='myprice']:checked").val();
            if (types == "奖")
            {
                //判断奖金是否够发  公式：选中的金额*表格里面的人数
                //表格里面的人数
                var tableId = document.getElementById("luckyPer");
                var tableLeng = tableId.rows.length - 1;//表格里的人数
                //选中的金额
                var je = $("input:radio[name='momey']:checked").val();
                if (je == "ok")
                {
                    //是其他金额
                    je=$("#qtmoney").val();

                }
                //alert("金额:" + je);
                //一共发放多少金额
                var totMoney = je * (1+tableLeng);
                //选中的奖金项
                var money = $("#money").val();
                var SelectMoney = money.split(',');
                //算钱是否够发
                var isok2 = SelectMoney[1] - totMoney;
                if (isok2 < 0)
                {
                    layer.msg('该奖金项余额不足', { time: 3000, icon: 5 });
                   
                    return false;
                }
                else
                {
                    //余额够的话就可以把刚才选中的人加到下面表格中
                    $(this).val("");
                    //alert("234:"+$(this).val());
                    $("#luckyPer").append("<tr><td>" + gh + "</td><td>" + ghName + "</td><td><img src='../Content/Images/del.png' class='delPerson' width='24' height='24' /></td>");
                    
                }
                

            }
            else
            {
                $(this).val("");
                //alert("234:" + $(this).val());
                $("#luckyPer").append("<tr><td>" + gh + "</td><td>" + ghName + "</td><td><img src='../Content/Images/del.png' class='delPerson' width='24' height='24' /></td>");
            }

            //$.post("/BonusOutput/Test", null, function (data)
            //{
                
            //})
            
        }
    });

    

    //添加常用原因
    $("#btnReson").click(function (data)
    {
        layer.open({
            title: "添加常用原因"
                        , type: 1
                        , skin: "layui-layer-lan"//layui-layer-lan layui-layer-molv
                        , area: ['50%', '25%']
                        , offset: "auto"
                        , content: $("#AddResonsBlock")
                        , cancel: function ()
                        {
                            //右上角关闭回调
                            $("#AddResonsBlock").hide();
                        }
                       , btn: ['确定', '取消']
                       , closeBtn: 2
                        , yes: function (index, layero)
                        {
                            
                            var CommReasons = $("#CommReasons").val();
                            //var Userid = $("#").val();
                            var Userid = $("#comeUserid").val();
                            var compid = $("#companyid").val();
                            alert(Userid);
                            $.post("/BonusOutput/AddBonusReson", { CommReasons: CommReasons, Userid: Userid, companyId: compid }, function (data)
                            {
                                if (data == "ok")
                                {
                                    $.ajax({
                                        type: "POST",
                                        url: '/BonusOutput/SelectBonusReson',//@Url.Action("SelectNoticeByXq", "TeachBuidsMain")
                                        data: { Userid: Userid },
                                        datatype: "json",
                                        success: function (data)
                                        {
                                            $('#cyyy').html(data);
                                            layer.close(idfsss);
                                        },
                                        beforeSend: function ()
                                        {
                                            //$("#sendGuangBoBlock").hide();
                                            //$("#cyyy").show();

                                            //加载层-风格4
                                            idfsss = layer.msg('加载中', {
                                                icon: 16
                                              , shade: 0.01
                                            });
                                        },
                                        error: function (XMLHttpRequest, textStatus, errorThrown)
                                        {
                                            layer.msg('错误', { time: 3000, icon: 5 });
                                        }

                                    });
                                    layer.close(index);
                                }
                                else
                                {
                                    layer.msg(data, { time: 3000, icon: 5 });

                                    return false;
                                }
                            });
                        }
        });
    });

    
});


