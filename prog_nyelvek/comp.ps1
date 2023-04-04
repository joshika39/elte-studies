if($args.Length -eq 0){
	Write-Output("The pattern is: 'ClassName' 'PackageName1' 'PackageName2' ...");
	return;
}

if($args.Length -gt 1){
	$pathDir = ""
	$packageStr = ""
	for ($i = 1; $i -lt $args.Count; $i++) {
		$arg = $args[$i];
		$pathDir += "$arg\"
		$packageStr += "$arg."
	}

	$className = $args[0];
	if(Test-Path -Path "$pathDir$className.java" -PathType Leaf){
		javac ".\$pathDir$className.java";
		java "$packageStr$className"
	}
	else{
		Write-Output(".\$pathDir$className.java does not exist")
	}

}
else{
	$className = $args[0]
	if(Test-Path -Path "$className.java" -PathType Leaf){
		javac "$className.java";
		java $args[0]
	}
	else{
		Write-Output("$className.java does not exist")
	}
}
