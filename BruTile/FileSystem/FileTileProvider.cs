﻿// Copyright (c) BruTile developers team. All rights reserved. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Threading.Tasks;
using BruTile.Cache;

namespace BruTile.FileSystem
{
    public class FileTileProvider : ITileProvider
    {
        private readonly FileCache _fileCache;

        public FileTileProvider(string directory, string format, TimeSpan cacheExpireTime)
        {
            _fileCache = new FileCache(directory, format, cacheExpireTime);
        }

        public FileTileProvider(FileCache fileCache)
        {
            _fileCache = fileCache;
        }

        public Task<byte[]> GetTileAsync(TileInfo tileInfo)
        {
            var bytes = _fileCache.Find(tileInfo.Index);
            if (bytes == null) throw new FileNotFoundException("The tile was not found at it's expected location");
            return Task.FromResult(bytes);
        }
    }
}
