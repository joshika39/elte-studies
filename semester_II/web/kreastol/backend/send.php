<?php

if(isset($_POST)){
	$email = $_POST['email'];
	$fName = $_POST['full-name'];
	$gender = $_POST['gender'];
	$comment = $_POST['comment'];

	echo "Email: " . $email . "<br>";
	echo "Full Name: " . $fName . "<br>";
	echo "Gender: " . $gender . "<br>";
	echo "Comment: " . $comment . "<br>";

	$replyMessage = "Az ön által beküldött megjegyzés:\n". $comment . "\n";


	$response = mail($email, "Köszönjük, hogy kinyúlt felénk!", $replyMessage);

	if($response){
		echo "Mail sent: " . $response;
	}
	else{
		echo "Mail has not been sent: " . $response;
	}
}