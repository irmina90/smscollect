<html>
<head>
<title>Online PHP Script Execution</title>
</head>
<body>
<?php

//echo "test";

if($_GET['MsgId']&&$_GET['status'] ) {

$myServer = "mssql.wmi.amu.edu.pl";
$myUser = "smscollect";
$myPass = "P6JMrSRu";

//connection to the database
$dbhandle = mssql_connect($myServer, $myUser, $myPass)
  or die("Couldn't connect to SQL Server on $myServer"); 


$arIds=explode(',',$_GET['MsgId']);
$arStatus=explode(',',$_GET['status']);
$arTo=explode(',',$_GET['to']);

if($arIds){
foreach($arIds as $k => $v ){
	//$query = "INSERT INTO statusy (sms_status,sms_index,sms_to) VALUES ('".$arStatus[$k]."','".$arIds[$k]."','".$arTo[$k]."')";
	$query = "UPDATE statusy SET sms_status = '".$arStatus[$k]."', sms_to = '".$arTo[$k]."' WHERE sms_index = '".$arIds[$k]."'";
	mssql_query($query);
	if( $arStatus[$k] == "404")
	{	
		$select_msg = "SELECT id_mgs FROM statusy WHERE sms_index = '".$arIds[$k]."'";
		$result = mssql_query($select_msg);
		$row = mssql_fetch_row($result);		
		$id_msg1 = $row[0];		
		mssql_free_result($result);
		
		$select_count = "SELECT COUNT(*) FROM statusy WHERE id_mgs = '".$id_msg1."'";
		$result1 = mssql_query($select_count);
		$count = mssql_num_rows($result1);
		mssql_free_result($result1);
		
		$update = "UPDATE tresc_sms SET Ilosc_wyslanych = ".$count." WHERE Id_tresc = '".$id_msg1."'";
		mssql_query($update);
		
	}
	
}

//close the connection
mssql_close($dbhandle);
echo "OK";
}
}
?>
</body>
</html>
