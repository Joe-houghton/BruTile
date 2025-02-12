﻿// Copyright (c) BruTile developers team. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using BruTile.Cache;
using BruTile.Web;

namespace BruTile.Predefined
{
    /// <summary>
    /// Known tile sources
    /// </summary>
    public enum KnownTileSource
    {
        OpenStreetMap,
        OpenCycleMap,
        OpenCycleMapTransport,
        BingAerial,
        BingHybrid,
        BingRoads,
        BingAerialStaging,
        BingHybridStaging,
        BingRoadsStaging,
        StamenToner,
        StamenTonerLite,
        StamenWatercolor,
        StamenTerrain,
        EsriWorldTopo,
        EsriWorldPhysical,
        EsriWorldShadedRelief,
        EsriWorldReferenceOverlay,
        EsriWorldTransportation,
        EsriWorldBoundariesAndPlaces,
        EsriWorldDarkGrayBase,
        BKGTopPlusColor,
        BKGTopPlusGrey
    }

    public static class KnownTileSources
    {
        private static readonly Attribution OpenStreetMapAttribution = new Attribution(
            "© OpenStreetMap contributors", "https://www.openstreetmap.org/copyright");
        private static readonly string CurrentYear = DateTime.Today.Year.ToString();
        private static readonly Attribution BKGAttribution = new Attribution("© Bundesamt für Kartographie und Geodäsie (" + CurrentYear + ")",
                         "https://sg.geodatenzentrum.de/web_public/Datenquellen_TopPlus_Open.pdf");

        /// <summary>
        /// Static factory method for known tile services
        /// </summary>
        /// <param name="source">The source</param>
        /// <param name="apiKey">An (optional) API key</param>
        /// <param name="persistentCache">A place to permanently store tiles (file of database)</param>
        /// <param name="tileFetcher">Option to override the web request</param>
        /// <param name="userAgent">The 'User-Agent' http header added to the tile request</param>
        /// <param name="minZoomLevel">The minimum zoom level</param>
        /// <param name="maxZoomLevel">The maximum zoom level</param>
        /// <returns>The tile source</returns>
        public static HttpTileSource Create(KnownTileSource source = KnownTileSource.OpenStreetMap, string apiKey = null,
            IPersistentCache<byte[]> persistentCache = null, Func<Uri, Task<byte[]>> tileFetcher = null, string userAgent = null,
            int minZoomLevel = 0, int maxZoomLevel = 20)
        {
            switch (source)
            {
                case KnownTileSource.OpenStreetMap:
                    return new HttpTileSource(new GlobalSphericalMercator(Math.Max(0, minZoomLevel), Math.Min(18, maxZoomLevel)),
                        "https://tile.openstreetmap.org/{z}/{x}/{y}.png",
                        name: source.ToString(),
                        persistentCache: persistentCache, tileFetcher: tileFetcher,
                        attribution: OpenStreetMapAttribution, userAgent: userAgent);
                case KnownTileSource.OpenCycleMap:
                    return new HttpTileSource(new GlobalSphericalMercator(Math.Max(0, minZoomLevel), Math.Min(17, maxZoomLevel)),
                        "http://{s}.tile.opencyclemap.org/cycle/{z}/{x}/{y}.png",
                        new[] { "a", "b", "c" }, name: source.ToString(),
                        persistentCache: persistentCache, tileFetcher: tileFetcher,
                        attribution: OpenStreetMapAttribution, userAgent: userAgent);
                case KnownTileSource.OpenCycleMapTransport:
                    return new HttpTileSource(new GlobalSphericalMercator(Math.Max(0, minZoomLevel), Math.Min(20, maxZoomLevel)),
                        "http://{s}.tile2.opencyclemap.org/transport/{z}/{x}/{y}.png",
                        new[] { "a", "b", "c" }, name: source.ToString(),
                        persistentCache: persistentCache, tileFetcher: tileFetcher,
                        attribution: OpenStreetMapAttribution, userAgent: userAgent);
                case KnownTileSource.BingAerial:
                    return new HttpTileSource(new GlobalSphericalMercator(Math.Max(1, minZoomLevel), Math.Min(19, maxZoomLevel)),
                        "https://t{s}.tiles.virtualearth.net/tiles/a{quadkey}.jpeg?g=517&token={k}",
                        new[] { "0", "1", "2", "3", "4", "5", "6", "7" }, apiKey, source.ToString(),
                        persistentCache, tileFetcher, new Attribution("© Microsoft"), userAgent);
                case KnownTileSource.BingHybrid:
                    return new HttpTileSource(new GlobalSphericalMercator(Math.Max(1, minZoomLevel), Math.Min(19, maxZoomLevel)),
                        "https://t{s}.tiles.virtualearth.net/tiles/h{quadkey}.jpeg?g=517&token={k}",
                        new[] { "0", "1", "2", "3", "4", "5", "6", "7" }, apiKey, source.ToString(),
                        persistentCache, tileFetcher, new Attribution("© Microsoft"), userAgent);
                case KnownTileSource.BingRoads:
                    return new HttpTileSource(new GlobalSphericalMercator(Math.Max(1, minZoomLevel), Math.Min(19, maxZoomLevel)),
                        "https://t{s}.tiles.virtualearth.net/tiles/r{quadkey}.jpeg?g=517&token={k}",
                        new[] { "0", "1", "2", "3", "4", "5", "6", "7" }, apiKey, source.ToString(),
                        persistentCache, tileFetcher, new Attribution("© Microsoft"), userAgent);
                case KnownTileSource.BingAerialStaging:
                    return new HttpTileSource(new GlobalSphericalMercator(Math.Max(1, minZoomLevel), Math.Min(19, maxZoomLevel)),
                        "http://t{s}.staging.tiles.virtualearth.net/tiles/a{quadkey}.jpeg?g=517&token={k}",
                        new[] { "0", "1", "2", "3", "4", "5", "6", "7" }, apiKey, source.ToString(),
                        persistentCache, tileFetcher, userAgent: userAgent);
                case KnownTileSource.BingHybridStaging:
                    return new HttpTileSource(new GlobalSphericalMercator(Math.Max(1, minZoomLevel), Math.Min(19, maxZoomLevel)),
                        "http://t{s}.staging.tiles.virtualearth.net/tiles/h{quadkey}.jpeg?g=517&token={k}",
                        new[] { "0", "1", "2", "3", "4", "5", "6", "7" }, apiKey, source.ToString(),
                        persistentCache, tileFetcher, userAgent: userAgent);
                case KnownTileSource.BingRoadsStaging:
                    return new HttpTileSource(new GlobalSphericalMercator(Math.Max(1, minZoomLevel), Math.Min(19, maxZoomLevel)),
                        "http://t{s}.staging.tiles.virtualearth.net/tiles/r{quadkey}.jpeg?g=517&token={k}",
                        new[] { "0", "1", "2", "3", "4", "5", "6", "7" }, apiKey, source.ToString(),
                        persistentCache, tileFetcher, userAgent: userAgent);
                case KnownTileSource.StamenToner:
                    return new HttpTileSource(new GlobalSphericalMercator(Math.Max(0, minZoomLevel), Math.Min(19, maxZoomLevel)),
                        "http://{s}.tile.stamen.com/toner/{z}/{x}/{y}.png",
                        new[] { "a", "b", "c", "d" }, name: source.ToString(),
                        persistentCache: persistentCache, tileFetcher: tileFetcher,
                        attribution: OpenStreetMapAttribution, userAgent: userAgent);
                case KnownTileSource.StamenTonerLite:
                    return new HttpTileSource(new GlobalSphericalMercator(Math.Max(0, minZoomLevel), Math.Min(19, maxZoomLevel)),
                        "http://{s}.tile.stamen.com/toner-lite/{z}/{x}/{y}.png",
                        new[] { "a", "b", "c", "d" }, name: source.ToString(),
                        persistentCache: persistentCache, tileFetcher: tileFetcher,
                        attribution: OpenStreetMapAttribution, userAgent: userAgent);
                case KnownTileSource.StamenWatercolor:
                    return new HttpTileSource(new GlobalSphericalMercator(Math.Max(0, minZoomLevel), Math.Min(19, maxZoomLevel)),
                        "http://{s}.tile.stamen.com/watercolor/{z}/{x}/{y}.png",
                        new[] { "a", "b", "c", "d" }, name: source.ToString(),
                        persistentCache: persistentCache, tileFetcher: tileFetcher,
                        attribution: OpenStreetMapAttribution, userAgent: userAgent);
                case KnownTileSource.StamenTerrain:
                    return
                        new HttpTileSource(
                            new GlobalSphericalMercator(Math.Max(4, minZoomLevel), Math.Min(19, maxZoomLevel))
                            {
                                Extent = new Extent(-14871588.04, 2196494.41775, -5831227.94199995, 10033429.95725)
                            },
                            "http://{s}.tile.stamen.com/terrain/{z}/{x}/{y}.png",
                            new[] { "a", "b", "c", "d" }, name: source.ToString(),
                            persistentCache: persistentCache, tileFetcher: tileFetcher,
                            attribution: OpenStreetMapAttribution, userAgent: userAgent);
                case KnownTileSource.EsriWorldTopo:
                    return new HttpTileSource(new GlobalSphericalMercator(Math.Max(0, minZoomLevel), Math.Min(19, maxZoomLevel)),
                        "https://server.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer/tile/{z}/{y}/{x}",
                        name: source.ToString(), persistentCache: persistentCache, tileFetcher: tileFetcher, userAgent: userAgent);
                case KnownTileSource.EsriWorldPhysical:
                    return new HttpTileSource(new GlobalSphericalMercator(Math.Max(0, minZoomLevel), Math.Min(8, maxZoomLevel)),
                        "https://server.arcgisonline.com/ArcGIS/rest/services/World_Physical_Map/MapServer/tile/{z}/{y}/{x}",
                        name: source.ToString(), persistentCache: persistentCache, tileFetcher: tileFetcher, userAgent: userAgent);
                case KnownTileSource.EsriWorldShadedRelief:
                    return new HttpTileSource(new GlobalSphericalMercator(Math.Max(0, minZoomLevel), Math.Min(13, maxZoomLevel)),
                        "https://server.arcgisonline.com/ArcGIS/rest/services/World_Shaded_Relief/MapServer/tile/{z}/{y}/{x}",
                        name: source.ToString(), persistentCache: persistentCache, tileFetcher: tileFetcher, userAgent: userAgent);
                case KnownTileSource.EsriWorldReferenceOverlay:
                    return new HttpTileSource(new GlobalSphericalMercator(Math.Max(0, minZoomLevel), Math.Min(13, maxZoomLevel)),
                        "https://server.arcgisonline.com/ArcGIS/rest/services/Reference/World_Reference_Overlay/MapServer/tile/{z}/{y}/{x}",
                        name: source.ToString(), persistentCache: persistentCache, tileFetcher: tileFetcher, userAgent: userAgent);
                case KnownTileSource.EsriWorldTransportation:
                    return new HttpTileSource(new GlobalSphericalMercator(Math.Max(0, minZoomLevel), Math.Min(19, maxZoomLevel)),
                        "https://server.arcgisonline.com/ArcGIS/rest/services/Reference/World_Transportation/MapServer/tile/{z}/{y}/{x}",
                        name: source.ToString(), persistentCache: persistentCache, tileFetcher: tileFetcher, userAgent: userAgent);
                case KnownTileSource.EsriWorldBoundariesAndPlaces:
                    return new HttpTileSource(new GlobalSphericalMercator(Math.Max(0, minZoomLevel), Math.Min(19, maxZoomLevel)),
                        "https://server.arcgisonline.com/ArcGIS/rest/services/Reference/World_Boundaries_and_Places/MapServer/tile/{z}/{y}/{x}",
                        name: source.ToString(), persistentCache: persistentCache, tileFetcher: tileFetcher, userAgent: userAgent);
                case KnownTileSource.EsriWorldDarkGrayBase:
                    return new HttpTileSource(new GlobalSphericalMercator(Math.Max(0, minZoomLevel), Math.Min(16, maxZoomLevel)),
                        "https://server.arcgisonline.com/arcgis/rest/services/Canvas/World_Dark_Gray_Base/MapServer/tile/{z}/{y}/{x}",
                        name: source.ToString(), persistentCache: persistentCache, tileFetcher: tileFetcher, userAgent: userAgent);
                case KnownTileSource.BKGTopPlusColor:
                    return new HttpTileSource(new GlobalSphericalMercator(Math.Max(0, minZoomLevel), Math.Min(19, maxZoomLevel)),
                        "https://sg.geodatenzentrum.de/wmts_topplus_open/tile/1.0.0/web_scale/default/WEBMERCATOR/{z}/{y}/{x}.png",
                        name: source.ToString(), persistentCache: persistentCache, tileFetcher: tileFetcher,
                        attribution: BKGAttribution, userAgent: userAgent);
                case KnownTileSource.BKGTopPlusGrey:
                    return new HttpTileSource(new GlobalSphericalMercator(Math.Max(0, minZoomLevel), Math.Min(19, maxZoomLevel)),
                        "https://sg.geodatenzentrum.de/wmts_topplus_open/tile/1.0.0/web_scale_grau/default/WEBMERCATOR/{z}/{y}/{x}.png",
                        name: source.ToString(), persistentCache: persistentCache, tileFetcher: tileFetcher,
                        attribution: BKGAttribution, userAgent: userAgent);
                default:
                    throw new NotSupportedException("KnownTileSource not known");
            }
        }
    }
}