<?php

    function mysqli_connect(
        $host = ini_get("localhost"),
        $user = ini_get("root"),
        $password = ini_get("t1eteoli"),
        $database = ini_get("barracao"),
        $port = ini_get(3306),
        $socket = ini_get('mysqli.default_socket')
        ): bool|mysqli { return true; }

?>