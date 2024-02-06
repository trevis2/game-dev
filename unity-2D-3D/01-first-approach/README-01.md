# First Approach

Sprite

A sprite is a 2d graphic object obtained from a bitmap image.

a bitmap image

Windows bitmap è un formato dati utilizzato per la rappresentazione di immagini raster sui sistemi operativi Microsoft Windows.
Le bitmap, come sono comunemente chiamati i file d'immagine di questo tipo, hanno generalmente l'estensione .bmp, o meno frequentemente .dib (device-independent bitmap).

Il formato di file Windows bitmap nella versione 3 permette operazioni di lettura e scrittura molto veloci e senza perdita di qualità, ma richiede generalmente una maggior quantità di memoria rispetto ad altri formati analoghi.

Le immagini bitmap possono avere una profondità di 1, 4, 8, 16, 24 o 32 bit per pixel. Le bitmap con 1, 4 e 8 bit contengono una tavolozza per la conversione dei (rispettivamente 2, 16 e 256) possibili indici numerici nei rispettivi colori. Nelle immagini con profondità più alta il colore non è indicizzato bensì codificato direttamente nelle sue componenti cromatiche RGB; con 16 o 32 bit per pixel alcuni bit possono rimanere inutilizzati.

Su disco le immagini bitmap sono codificate utilizzando alcune semplici strutture che ne descrivono le proprietà. Tutti i valori sono in ordine little endian.

---

method start
launched once at the start of game.
