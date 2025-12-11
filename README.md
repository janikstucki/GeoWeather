# GeoWeather

Eine Wetterstations-App mit interaktiver Karte (Leaflet + OpenStreetMap) in WPF.

## Karte

Die Karte wird mit [Leaflet](https://leafletjs.com/) erstellt.  
Die Kartendaten stammen von [OpenStreetMap](https://www.openstreetmap.org/) und werden unter der [ODbL-Lizenz](https://opendatacommons.org/licenses/odbl/) bereitgestellt.

```html
L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '© OpenStreetMap contributors'
}).addTo(map);
