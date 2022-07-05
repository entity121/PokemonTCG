<?php

    $connection = new mysqli(fgets(fopen("Datenbank_Informationen(php).txt","r")),"root","","pokemon tcg");
    $connection->set_charset("utf8mb4");
    if($connection->connect_error){die("Verbindung fehlgeschlagen");}

    
    // Die Werte aus der Datenbank speichern und auf anderer Datenbank nutzen
    if($_GET['req']=="db_speichern"){

        $allRows = array();

        $sql = "SELECT * FROM karten";
        $erg = $connection->query($sql);

        $file = fopen("Datenbank_Inhalt.txt","w");
        $string = "";
    
        while($row = $erg->fetch_assoc()){

            $string.=json_encode($row).PHP_EOL;
        
        }
        
        fwrite($file,$string);
        fclose($file);
        
    }



    
    else if($_GET['req']=="db_laden"){

        // Die Stelle ermitteln, an der die Datenbank endet und ab welcher die neuen Einträge hinzugefügt werden sollen
        //###################
        $stelle = 0;

        $sql = "SELECT * FROM karten";
        $erg = $connection->query($sql);

        while($erg->fetch_assoc()){
            $stelle += 1;
        }
        //###################



        $file = fopen("Datenbank_Inhalt.txt","r");

        while(($line = fgets($file))!=false){

            $line = json_decode($line);

            if($line->ID > $stelle){

                $sql = "INSERT INTO karten (
                ID,Art,Kartenname,Vorentwicklung,Weiterentwicklung,Typ,KP,Fähigkeit,
                Angriff1,Kosten1,Energie1,Farblos1,Schaden1,Fähigkeit1,
                Angriff2,Kosten2,Energie2,Farblos2,Schaden2,Fähigkeit2,
                Schwäche,Resistenz,Rückzugskosten,DexNummer,KartenNummer,Booster,BasisEnergie)
                VALUES (
                $line->ID,'$line->Art','$line->Kartenname','$line->Vorentwicklung','$line->Weiterentwicklung','$line->Typ',$line->KP,$line->Fähigkeit,
                '$line->Angriff1',$line->Kosten1,'$line->Energie1',$line->Farblos1,$line->Schaden1,$line->Fähigkeit1,
                '$line->Angriff2',$line->Kosten2,'$line->Energie2',$line->Farblos2,$line->Schaden2,$line->Fähigkeit2,
                '$line->Schwäche','$line->Resistenz',$line->Rückzugskosten,$line->DexNummer,$line->KartenNummer,'$line->Booster',$line->BasisEnergie)";

                $erg = $connection->query($sql);

                echo $erg;

            }
            

        }

        fclose($file);

    }

?>