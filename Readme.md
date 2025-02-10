# OpenRails - TSRE - Proxy d'imagerie satellite

Cet outil est un proxy web qui permet de rediriger les requêtes de TSRE vers differents services d'imagerie satellite.

## Comment l'utiliser

### Installation

Voir la selection [release](#release) pour le téchargement du zip.
Le zip est à extraire n'importe où.

### Configuration rapide du proxy d'imagerie satellite

Avant de lancer l'application, il peut être souhaitable de configurer l'emplacement du répertoire temporaire où les images satellites vont être téléchargées.
Pour ce faire, il faut éditer le fichier **GeoImageryProxy.exe.config**, avec un Notepad (Notepad++ par exemple) et changer le chemin indiqué là

```
<setting name="ImageryFolder" serializeAs="String">
	<value>C:\Temp\GeoImageryProxy</value>
</setting>
```

Ce dossier ou ses sous-dossiers peuvent être supprimés à souhait bien entendu pour faire de la place sur le disque dur.

### Configuration de TSRE

Il faut configurer TSRE pour le faire pointer vers le proxy d'imagerie satellite.
Les instructions sont indiqués dans la fenêtre pour configurer TSRE, il suffit de modifier le fichier settings.txt avec le paramètre

```
imageMapsUrl = http://localhost:8000/?lon={lon}&lat={lat}&zoom={zoom}&res={res}
```

### Lancer et tester le proxy d'imagerie satellite

Il suffit simplement de lancer **GeoImageryProxy.exe**

![Capture d'écran](https://i.postimg.cc/023TKT7g/Capture-d-cran-2025-02-10-224402.png)

Cliquer sur le lien **Test me** devrait ouvrir le navigateur web par défaut et affichera une image générée par le proxy.
L'image devra être centrée sur ce qu'on appellera entre nous, "une étoile de calibration cosmo tellurique".
Ceci permet de vérifier le fonctionnement et qu'il n'y a pas de décalage des coordonnées des images.

Le fournisseur d'imagerie satellite peut être sélectionné depuis la liste déroulante, il n'est pas nécessaire de redémarrer le proxy ni TSRE après ce changement.

Il est nécessaire de lancer GeoImageryProxy.exe uniquement si on veut utiliser les options Geo, Map, Z17,Z18 de TSRE.

#### Google Map

![Google Map](https://i.postimg.cc/PJnHD0gr/localhost.jpg)

#### Bing

![Bing](https://i.postimg.cc/hjkmdP4z/localhost2.jpg)

### Configuration avancée

Le fichier **GeoImageryProxy.exe.config** contient d'autres paramètres, en particulier:
- **PortNumber** est le port d'écoute du proxy. Il n'est pas nécessaire de le changer, à moins que le port soit déjà pris par un autre programme.
- **UserAgent** et **ForceUserAgent** sont utilisés conjointement pour forcer le user agent que le proxy va utiliser pour se connecter aux services d'imagerie. C'est nécessaire pour Google.
- **DemFolder** n'est pas utilisé pour le moment. La fonction DEM n'est pas implémentée.

## Exemple d'utilisation

### Exemple 1
- La tuile d'en haut; distant terrain, Z13
- La tuile à gauche: OSM natif de TSRE5
- La tuile à droite: Bing Map, Z18
![Exemple 1](https://zupimages.net/up/22/13/z3hr.png)

### Exemple 2
- La tuile en bas à droite: detailed terrain, Z18, IGN
- La tuile en haut: detailed terrain, Z18, IGN 1950 (la fameuse plus grande gare de fret de la région Est qui a été rasée de la carte vers 1990)
- La tuile de fond à gauche: distant terrain, Z13
![Exemple 2](https://zupimages.net/up/22/13/tn4f.png)

## Release

- 2022-04-06 - [beta](https://github.com/Bruno-Muller/ORTS-TSRE-GeoImageryProxy/releases/download/beta/GeoImageryProxy.zip)