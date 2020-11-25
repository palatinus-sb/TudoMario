﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TudoMario.Map
{
    public enum ColliderOption
    {
        GenerateCollider
    }
    public sealed partial class Chunk : UserControl
    {
        public Tile[,] Tiles = new Tile[16, 16];
        /// <summary>
        /// X,Y coords of chunk in the registered map.
        /// </summary>
        public Vector2 ChunkPosition { get; set; }
        public Chunk()
        {
            InitializeComponent();
            Height = 512;
            Width = 512;
        }

        /// <summary>
        /// Sets the tile at the given coordinates in this chunk to the given tile type.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetTileAt(int x, int y, Tile tile)
        {
            //0 is the top since we go from top left so it has to be mirrored
            y = 15 - y;

            RemoveTileFromChunk(Tiles[x, y]);
            Tiles[x, y] = tile;

            ChunkCanvas.Children.Add(tile);
            Canvas.SetLeft(tile, (x * 32));
            Canvas.SetTop(tile, (y * 32));
        }
        /// <summary>
        /// Sets the tile at the given coordinates in this chunk to the given tile type and registers a collider for it.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tileType"></param>
        /// <param name="imagePath"></param>
        public void SetTileAt(int x, int y, Tile tile, ColliderOption opt)
        {
            //0 is the top since we go from top left so it has to be mirrored
            y = 15 - y;

            RemoveTileFromChunk(Tiles[x, y]);
            if (opt == ColliderOption.GenerateCollider)
                GenerateColliderForTile(tile, x, y);
            Tiles[x, y] = tile;

            ChunkCanvas.Children.Add(tile);
            Canvas.SetLeft(tile, (x * 32));
            Canvas.SetTop(tile, (y * 32));
        }
        /// <summary>
        /// Creates a new tile at the given coordinates and the given textures.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="texture"></param>
        public void SetTileAt(int x, int y, BitmapImage texture)
        {
            //0 is the top since we go from top left so it has to be mirrored
            y = 15 - y;

            Tile tile = new Tile();
            tile.Texture = texture;

            RemoveTileFromChunk(Tiles[x, y]);
            Tiles[x, y] = tile;

            ChunkCanvas.Children.Add(tile);
            Canvas.SetLeft(tile, (x * 32));
            Canvas.SetTop(tile, (y * 32));
        }
        /// <summary>
        /// Creates a new tile at the given coordinates and the given textures and creates a collider for it.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="texture"></param>
        /// <param name="opt"></param>
        public void SetTileAt(int x, int y, BitmapImage texture, ColliderOption opt)
        {
            //0 is the top since we go from top left so it has to be mirrored
            y = 15 - y;

            Tile tile = new Tile();
            tile.Texture = texture;

            RemoveTileFromChunk(Tiles[x, y]);
            if (opt == ColliderOption.GenerateCollider)
                GenerateColliderForTile(tile, x, y);
            Tiles[x, y] = tile;

            ChunkCanvas.Children.Add(tile);
            Canvas.SetLeft(tile, (x * 32));
            Canvas.SetTop(tile, (y * 32));
        }

        /// <summary>
        /// Fills this chunk with the given tiletype
        /// </summary>
        /// <param name="tileType"></param>
        /// <param name="imagePath"></param>
        public void FillChunkWith(BitmapImage texture)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    i = 15 - i;

                    Tile _tile = (Tile)Activator.CreateInstance(typeof(Tile));
                    //_tile.SetBackgroundColor(TileFillerBrush);
                    _tile.Texture = texture;

                    Tiles[j, i] = _tile;
                    ChunkCanvas.Children.Add(_tile);
                    Canvas.SetLeft(_tile, (j * 32));
                    Canvas.SetTop(_tile, (i * 32));
                }
            }
        }

        private void RemoveTileFromChunk(Tile target)
        {
            /// TODO
            //Has to remove collider after implementation

            if (target != null && ChunkCanvas.Children.Contains(target))
                ChunkCanvas.Children.Remove(target);
        }

        private void GenerateColliderForTile(Tile tile, int x, int y)
        {

        }
        private void GetLogicalCenterOfTile()
        {

        }
    }
}
