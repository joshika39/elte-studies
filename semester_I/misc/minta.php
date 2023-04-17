<?php

$date = date("Y-M-d")

?>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="UTF-8">
	<meta http-equiv="X-UA-Compatible" content="IE=edge">
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<title>Document</title>
</head>
<body>
	<h1>Minta doksi</h1>
	<p>Mai datum: <?php echo $date ?></p>
	<p>Ido: <?php echo date("h:i:sa") ?></p>
	<p style="position: absolute; bottom: 0;">&#169 Copyright <?php echo date("Y") ?> Joshua</p>
</body>
</html>