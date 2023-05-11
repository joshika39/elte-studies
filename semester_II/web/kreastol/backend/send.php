<?php

use PHPMailer\PHPMailer\PHPMailer;
use PHPMailer\PHPMailer\Exception;

require_once 'mail/Exception.php';
require_once 'mail/SMTP.php';
require_once 'mail/PHPMailer.php';


if ($_SERVER['REQUEST_METHOD'] === 'POST') {
	session_start();

	$email = $_POST['email'];
	$fName = $_POST['full-name'];
	$gender = $_POST['gender'];
	$comment = $_POST['comment'];

	$replyMessage = "Az ön által beküldött megjegyzés:\n" . $comment . "\n";

	try {
		$mail = new PHPMailer();

		$mail->SMTPDebug = 0;
		$mail->isSMTP();
		$mail->Host = 'smtp-relay.sendinblue.com';
		$mail->SMTPAuth = true;
		$mail->Username = 'jhegedus9@gmail.com';
		$mail->Password = '*****';
		$mail->SMTPSecure = 'tls';
		$mail->Port = 587;
		$mail->CharSet = "UTF-8";

		$mail->setFrom('joshua@kreastol.club', 'Joshua Hegedus');
		$mail->addAddress($email, $fName);

		$mail->isHTML(true);
		$mail->Subject = 'Köszönjük a visszajelzését';
		$mail->Body = strip_tags($replyMessage);
		$mail->AltBody = strip_tags($replyMessage);

		$mail->send();

		$_SESSION['message'] = "sent";
		$_SESSION['shown'] = false;
		
		header("Location: ../contact.php");

	} catch (Exception $e) {
		echo $e;
	}
}