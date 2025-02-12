﻿// Copyright (c) BruTile developers team. All rights reserved. See License.txt in the project root for license information.

using System;

namespace BruTile.Wmts
{
    /// <summary>
    /// An identifier for the crs
    /// </summary>
    public readonly struct CrsIdentifier : IEquatable<CrsIdentifier>
    {
        public static bool TryParse(string urnOgcDefCRS, out CrsIdentifier crs)
        {
            var parts = urnOgcDefCRS.Split(':');
            switch (parts.Length)
            {
                case 2:
                    crs = new CrsIdentifier(parts[0], "", parts[1]);
                    break;
                case 6:
                    crs = new CrsIdentifier(parts[4], "", parts[5]);
                    break;
                case 7:
                    crs = new CrsIdentifier(parts[4], parts[5], parts[6]);
                    break;
                default:
                    crs = new CrsIdentifier();
                    break;
            }

            return !string.IsNullOrEmpty(crs.Authority);
        }

        /// <summary>
        /// Initializes this coordinate system identifier
        /// </summary>
        /// <param name="authority">The authority</param>
        /// <param name="version">The version</param>
        /// <param name="identifier">The identifier</param>
        internal CrsIdentifier(string authority, string version, string identifier)
        {
            Authority = authority;
            Version = version;
            Identifier = identifier;
        }

        /// <summary>
        /// The authority
        /// </summary>
        public string Authority { get; }

        /// <summary>
        /// The identifier
        /// </summary>
        public string Identifier { get; }

        /// <summary>
        /// The version
        /// </summary>
        public string Version { get; }

        public override string ToString()
        {
            return $"urn:ogc:def:crs:{Authority}:{Version}:{Identifier}";
        }

        public bool Equals(CrsIdentifier other)
        {
            if (Authority != other.Authority) return false;
            if (Version != other.Version) return false;
            if (Identifier != other.Identifier) return false;
            return true;
        }
    }
}