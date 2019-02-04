using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Audio.OpenAL;
using Minesweeper3D;
using Test123;

namespace Test123
{
    class Game
    {
        private GameWindow gm;
        private GameState gs;
        private Graphics3D gp;
        private SoundSystem ss;
        private GameData gd;
        public double theta { get; private set; }

        private Matrix4 perspective;

        private Random rand;

        private Menu currentMenu;
    
        private Menu mainMenu;
        private Menu playMenu;
        private Menu pauseMenu;
        private Menu customMapMenu;

        private int[] customData;

        private int selectedMenuItem;
        private int previousSelectedMenuItem;

        private bool cheats;
        private bool showGameStatus;

        private VectorXYZ deltaMove;
        private VectorXYZ desiredMove;
        
        private VectorXYZ deltaMenuItemSize;
        private VectorXYZ deltaMenuItemPos;
        private VectorXYZ deltaPreviousMenuItemSize;
        private VectorXYZ deltaPreviousMenuItemPos;

        private double yMenuPos;
        private double yMenuDesired;

        

        public Game(GameWindow gameWindow, GameData gameData)
        {
            this.gp = new Graphics3D(StaticTextureList.TexturePaths);
            this.gm = gameWindow;
            this.gd = gameData;
            this.ss = new SoundSystem(StaticSoundList.soundPaths);
            this.gs = GameState.Helpscreen;
            this.rand = new Random();

            ResetValues();
            ResetMenuSizeValues();
            ResetMenuMoveValues();

            this.selectedMenuItem = 0;
            this.mainMenu = new Menu(StaticMenuDatabase.mainMenu);
            this.currentMenu = mainMenu;

            this.playMenu = new Menu(StaticMenuDatabase.playMenu); playMenu.superiorMenu = mainMenu;
            this.pauseMenu = new Menu(StaticMenuDatabase.pauseMenu); 
            this.customMapMenu = new Menu(StaticMenuDatabase.customMapMenu); customMapMenu.superiorMenu = playMenu;

            this.customData = new int[4]{ 0, 0, 0, 0 }; // for a later myself, these are values assigned by custom map menu input, i was just fucking bad at naming variables that night...

            this.cheats = false;
            this.showGameStatus = false;

            Start(); // just wondering what this may do
        }

        private void ResetMenuMoveValues()
        {
            this.yMenuPos = 0;
            this.yMenuDesired = 0;
        }
        private void ResetMenuSizeValues()
        {
            this.deltaMenuItemSize = new VectorXYZ(21.0, 7.0, 2.0);
            this.deltaPreviousMenuItemSize = new VectorXYZ(24.0, 8.0, 3.0);
            this.deltaMenuItemPos = new VectorXYZ(0.0, 0.0, 0.0, 0.0);
            this.deltaPreviousMenuItemPos = new VectorXYZ(-13.0, 0.0, 5.0, 26.0);
        }
        private void ResetValues()
        {
            desiredMove = new VectorXYZ(0.0, 0.0, -350.0, 35.0);
            deltaMove = new VectorXYZ(0.0, 0.0, -275.0, 35.0);
            this.theta = 35;
        }

        private void CreateNewGame(int width, int height, int mines)
        {
            ResetValues();
            gd = new GameData(width, height, mines);
            selectedMenuItem = 0;
            gs = GameState.InGame;
            selectedMenuItem = 0;
        }

        private void CreateNewGame(int width, int height, int mines, int seed)
        {
            ResetValues();
            gd = new GameData(width, height, mines, seed);
            selectedMenuItem = 0;
            gs = GameState.InGame;
            selectedMenuItem = 0;
        }

        private void Loaded(Object o, EventArgs e)
        {
            GL.ClearColor(0.2f, 0.2f, 0.2f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.ColorMaterial);

            GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 0.2f, 0.2f, 0.2f });
            //GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 0.05f, 0.05f, 0.0f });
            GL.Light(LightName.Light0, LightParameter.Position, new float[] { 10.0f, 10.0f, 100.0f });

            GL.Enable(EnableCap.Light0);

            GL.Enable(EnableCap.Texture2D);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            
        }

        private void Start()
        {
            gm.Load += Loaded;
            gm.Resize += Resized;
            gm.UpdateFrame += UpdateF;
            gm.KeyPress += KeyPressed;
            gm.Run(1 / 60.0);
        }

        private void KeyPressed(Object o, KeyPressEventArgs e)
        {
            if(gs == GameState.Win)
            {
                switch(e.KeyChar)
                {
                    case 'q':
                    case 'e':
                        selectedMenuItem = 0;
                        ss.PlayAndResetSound(1);
                        ResetMenuMoveValues();
                        currentMenu = mainMenu; gs = GameState.InMenu;
                        break;
                }
            }
            else if (gs == GameState.InGame || gs == GameState.Lose)
            {
                showGameStatus = false;
                switch (e.KeyChar)
                {
                    case 'w':
                        if (gd.selected.y < gd.size.y - 1)
                        {
                            desiredMove.y -= 20;
                            gd.selected.y++;
                        }
                        break;
                    case 's':
                        if (gd.selected.y > 0)
                        {
                            desiredMove.y += 20;
                            gd.selected.y--;
                        }
                        break;
                    case 'a':
                        if (gd.selected.x > 0)
                        {
                            desiredMove.x += 20;
                            //xDelta += 20;
                            gd.selected.x--;
                        }
                        break;
                    case 'd':
                        if (gd.selected.x < gd.size.x - 1)
                        {
                            desiredMove.x -= 20;
                            gd.selected.x++;
                        }
                        break;
                    case 'i':
                        desiredMove.z += 50;
                        break;
                    case 'k':
                        desiredMove.z -= 50;
                        break;
                    case 'e':
                        if (gs == GameState.InGame)
                        {
                            if (gd.map[gd.selected.x, gd.selected.y].isMine) { gd.stopwatch.Stop(); gs = GameState.Lose; showGameStatus = true; }
                            else { gd.StackExplode(gd.selected.x, gd.selected.y); }
                        }
                        else { ss.PlayAndResetSound(1); currentMenu = mainMenu; gs = GameState.InMenu; }
                        break;
                    case 'f':
                        if(gs == GameState.InGame) gd.Flag(gd.selected.x, gd.selected.y);
                        break;
                    case 'j':
                        desiredMove.theta += 15;
                        break;
                    case 'l':
                        desiredMove.theta -= 15;
                        break;
                    case 'q':
                        ss.PlayAndResetSound(1);
                        if (gs == GameState.InGame)
                        {
                            selectedMenuItem = 0;
                            gs = GameState.InMenu;
                            currentMenu = pauseMenu;
                        }
                        else { currentMenu = mainMenu; gs = GameState.InMenu; }
                        break;
                    case 'c':
                        cheats = (cheats) ? false : true;
                        break;
                }
                if(gd.cellsLeft == gd.minesSet) { gd.stopwatch.Stop(); gs = GameState.Win; }
            }
            else if(gs == GameState.InMenu)
            {
                switch(e.KeyChar)
                {
                    case 'w':
                        ss.PlayAndResetSound(0);
                        if (selectedMenuItem > 0) { previousSelectedMenuItem = selectedMenuItem; selectedMenuItem--; this.yMenuDesired -= 10;  } else { previousSelectedMenuItem = selectedMenuItem; selectedMenuItem = currentMenu.menuItems.Length - 1; this.yMenuDesired = 10 * selectedMenuItem; }
                        ResetMenuSizeValues();
                        break;
                    case 's':
                        ss.PlayAndResetSound(0);
                        ResetMenuSizeValues();
                        if (selectedMenuItem < currentMenu.menuItems.Length - 1) { previousSelectedMenuItem = selectedMenuItem; selectedMenuItem++; this.yMenuDesired += 10; } else { previousSelectedMenuItem = selectedMenuItem; selectedMenuItem = 0; this.yMenuDesired = 0; }
                        break;
                    case 'e':
                        if(currentMenu == mainMenu)
                        {
                            switch (selectedMenuItem) {
                                case 0:
                                    ResetMenuMoveValues();
                                    ss.PlayAndResetSound(1);
                                    selectedMenuItem = 0;
                                    currentMenu = playMenu;
                                    break;
                                case 1:
                                    ResetMenuMoveValues();
                                    ss.PlayAndResetSound(1);
                                    selectedMenuItem = 0;
                                    gs = GameState.Helpscreen;
                                    break;
                                case 2:
                                    ResetMenuMoveValues();
                                    ss.PlayAndResetSound(1);
                                    selectedMenuItem = 0;
                                    gs = GameState.Credits;
                                    break;
                                case 3:
                                    if(ss.soundEnabled)
                                    {
                                        mainMenu[selectedMenuItem].ChangeTexture(46);
                                        ss.soundEnabled = false;
                                    } else { ss.soundEnabled = true; ss.PlayAndResetSound(1); mainMenu[selectedMenuItem].ChangeTexture(45); }
                                    break;
                                case 4:
                                    gm.Close();
                                    break;
                            }
                            
                        }
                        else if(currentMenu == playMenu)
                        {
                            ResetMenuMoveValues();
                            ss.PlayAndResetSound(1);
                            switch (selectedMenuItem)
                            {
                                case 0:
                                    CreateNewGame(5, 5, 3);
                                    break;
                                case 1:
                                    CreateNewGame(8, 8, 10);
                                    break;
                                case 2:
                                    CreateNewGame(15, 15, 30);
                                    break;
                                case 3:
                                    CreateNewGame(30, 30, 100);
                                    break;
                                case 4:
                                    selectedMenuItem = 0;
                                    currentMenu = customMapMenu;
                                    previousSelectedMenuItem = 0;
                                    break;
                                case 5:
                                    Random gameGenerator = new Random();
                                    int width = gameGenerator.Next(3, 50);
                                    int height = gameGenerator.Next(3, 50);
                                    int mines = gameGenerator.Next(3, (width * height) - 1);
                                    int gameSeed = gameGenerator.Next(0, 9999);
                                    CreateNewGame(width, height, mines, gameSeed);
                                    break;
                            }
                        }
                        else if(currentMenu == customMapMenu)
                        {
                            switch(selectedMenuItem)
                            {
                                case 3:
                                    ss.PlayAndResetSound(1);
                                    Random rand = new Random();
                                    customData[3] = rand.Next(0, 9999);
                                    break;
                                case 4:
                                    if ((customData[0] > 0 && customData[1] > 0 && customData[2] > 0) && customData[0] * customData[1] > customData[2])
                                    {
                                        ResetMenuMoveValues();
                                        ss.PlayAndResetSound(1);
                                        ResetValues();
                                        if(customData[3] > 0) gd = new GameData(customData[0], customData[1], customData[2], customData[3]);
                                        else gd = new GameData(customData[0], customData[1], customData[2]);
                                        gs = GameState.InGame;
                                    }
                                    else
                                    {
                                        ResetMenuMoveValues();
                                        selectedMenuItem = 0;
                                        ss.PlayAndResetSound(2);
                                    }
                                    break;
                            }
                        }
                        else if(currentMenu == pauseMenu)
                        {
                            ss.PlayAndResetSound(1);
                            switch (selectedMenuItem)
                            {
                                case 0:
                                    gs = GameState.InGame;
                                    break;
                                case 1:
                                    ResetMenuMoveValues();
                                    currentMenu = mainMenu;
                                    selectedMenuItem = 0;
                                    break;
                                case 2:
                                    gm.Close();
                                    break;
                            }
                            selectedMenuItem = 0;
                        }
                        break;
                    case 'q':
                        ResetMenuMoveValues();
                        ss.PlayAndResetSound(2);
                        if (currentMenu == mainMenu) { gm.Close(); }
                        else if (currentMenu == playMenu || currentMenu == customMapMenu) currentMenu = currentMenu.superiorMenu;
                        selectedMenuItem = 0;
                        break;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        int allowedLength = (selectedMenuItem < 2)?2:4;
                        if (currentMenu == customMapMenu)
                        {
                            if (customData[selectedMenuItem].ToString().Length < allowedLength)
                            {
                                ss.PlayAndResetSound(1);
                                customData[selectedMenuItem] = (customData[selectedMenuItem] * 10) + (e.KeyChar - 48);
                            }
                            else
                            {
                                ss.PlayAndResetSound(2);
                            }
                        }
                        break;
                    case 'b':
                        if (currentMenu == customMapMenu)
                        {
                            ss.PlayAndResetSound(1);
                            customData[selectedMenuItem] = customData[selectedMenuItem] / 10;
                            if (customData[selectedMenuItem] < 0) customData[selectedMenuItem] = 0;
                        }
                        break;
                }
            }
            else if(gs == GameState.Helpscreen || gs == GameState.Credits)
            {
                switch(e.KeyChar)
                {
                    case 'q':
                    case 'e':
                        ss.PlayAndResetSound(1);
                        gs = GameState.InMenu;
                        break;
                }
            }
        }

        private void Resized(Object o, EventArgs e)
        {
            GL.Viewport(0, 0, gm.Width, gm.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            this.perspective = Matrix4.CreatePerspectiveFieldOfView(0.55f, (float)gm.Width/(float)gm.Height, 1.0f, 3000.0f); //maybe lower down far clip plane...
            GL.LoadMatrix(ref this.perspective);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        private void UpdateF(Object o, EventArgs e)
        {
            GL.LoadIdentity();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (gs == GameState.InGame) { gp.DrawHUD(gd.minesSet, gd.cellsLeft, gd.flags, gd.seed, (int)(gd.stopwatch.Elapsed).TotalSeconds, gd.size.x, gd.size.y); RenderField(false); }
            else if (gs == GameState.InMenu) RenderMenu(currentMenu, selectedMenuItem);
            else if (gs == GameState.Helpscreen) StatusScreen(32, false);
            else if (gs == GameState.Lose) { if (showGameStatus) StatusScreen(40, true); RenderField(true); }
            else if (gs == GameState.Win) { StatusScreen(39, true); }
            else if (gs == GameState.Credits) { StatusScreen(44, false); }

            gm.SwapBuffers();
        }

        private void StatusScreen(int textureId, bool showTimeElapsed)
        {
            GL.Translate(0.0, 0.0, -100);
            GL.Rotate(25.0, 1.0, 1.0, 0.0);
            double yOffset = (showTimeElapsed) ? 3.0 : 0.0;
            gp.DrawMenuItem(0.0, yOffset, 0.0, 60.0, 40.0, 2.0, textureId, false);
            if(showTimeElapsed)
            {
                gp.DrawMenuItem(-20.0, -21.5, 1.0, 20.0, 5.0, 2.0, 34, false);
                gp.DrawWhiteNum((int)gd.stopwatch.Elapsed.TotalSeconds, -5.0, -21.5, 1.0, 5.0, true);
            }
        }

        private void RenderMenu(Menu menu, int selectedMenuItem)
        {
            yMenuPos = Animate(yMenuPos, yMenuDesired, 1.0);
            GL.Translate(0.0, yMenuPos + 10, -100);
            GL.Rotate(25.0, 1.0, 1.0, 0.0);

            for (int i = 0; i < menu.Length; i++)
            {

                if (i == selectedMenuItem)
                {
                    if (menu[selectedMenuItem].editable)
                    {
                        AnimateVectorXYZ(this.deltaMenuItemSize, new VectorXYZ(24.0, 8.0, 3.0), 0.5, 0.25, 0.25, 1.0);
                        AnimateVectorXYZ(this.deltaMenuItemPos, new VectorXYZ(-13.0, 0.0, 0.0, 26.0), 1.0, 0.5, 0.5, 2.0);
                        GL.Rotate(this.deltaMenuItemPos.theta, 0.0, 1.0, 0.0);
                        gp.DrawMenuItem(-12.0 + this.deltaMenuItemPos.x, selectedMenuItem * -10, 0.0, this.deltaMenuItemSize.x, this.deltaMenuItemSize.y, this.deltaMenuItemSize.z, menu[selectedMenuItem].textureId, true);
                        GL.Rotate((-1) * this.deltaMenuItemPos.theta, 0.0, 1.0, 0.0);
                        gp.DrawWhiteNum(customData[i], 7.0 + deltaMenuItemPos.x / 2, i * -10, 0.0, 8.0, false);
                    }
                    else if (!menu[selectedMenuItem].editable)
                    {
                        AnimateVectorXYZ(this.deltaMenuItemSize, new VectorXYZ(24.0, 8.0, 3.0), 0.5, 0.25, 0.2, 1.0);
                        AnimateVectorXYZ(this.deltaMenuItemPos, new VectorXYZ(0.0, 0.0, 5.0), 0.5, 0.5, 0.5, 1.0);
                        gp.DrawMenuItem(0.0, selectedMenuItem * -10, 0.0 + this.deltaMenuItemPos.z, this.deltaMenuItemSize.x, this.deltaMenuItemSize.y, this.deltaMenuItemSize.z, menu[selectedMenuItem].textureId, true);
                    }
                }
                else
                {
                    if (i != previousSelectedMenuItem)
                    {
                        if (menu[i].editable) { gp.DrawMenuItem(-12.0, i * -10, 0.0, 21.0, 7.0, 2.0, menu[i].textureId, false); gp.DrawWhiteNum(customData[i], 7.0, i * -10, 0.0, 8.0, false); }
                        else gp.DrawMenuItem(0.0, i * -10, 0.0, 21.0, 7.0, 2.0, menu[i].textureId, false);
                    }
                    else if (i == previousSelectedMenuItem)
                    {
                        if (menu[i].editable)
                        {
                            AnimateVectorXYZ(this.deltaPreviousMenuItemPos, new VectorXYZ(0.0, 0.0, 0.0, 0.0), 1.0, 0.5, 0.5, 2.0);
                            AnimateVectorXYZ(this.deltaPreviousMenuItemSize, new VectorXYZ(21.0, 7.0, 2.0), 0.5, 0.25, 0.2, 1.0);
                            GL.Rotate(this.deltaPreviousMenuItemPos.theta, 0.0, 1.0, 0.0);
                            gp.DrawMenuItem(-12.0 + this.deltaPreviousMenuItemPos.x, i * -10, 0.0, this.deltaPreviousMenuItemSize.x, this.deltaPreviousMenuItemSize.y, this.deltaPreviousMenuItemSize.z, menu[i].textureId, false);
                            GL.Rotate((-1) * (this.deltaPreviousMenuItemPos.theta), 0.0, 1.0, 0.0);
                            gp.DrawWhiteNum(customData[i], 7.0 + this.deltaPreviousMenuItemPos.x / 2, i * -10, 0.0, 8.0, false);
                        }
                        else
                        {
                            AnimateVectorXYZ(this.deltaPreviousMenuItemSize, new VectorXYZ(21.0, 7.0, 2.0), 0.5, 0.25, 0.2, 1.0);
                            AnimateVectorXYZ(this.deltaPreviousMenuItemPos, new VectorXYZ(0.0, 0.0, 0.0), 0.5, 0.5, 0.5, 1.0);
                            gp.DrawMenuItem(0.0, i * -10, 0.0 + this.deltaPreviousMenuItemPos.z, this.deltaPreviousMenuItemSize.x, this.deltaPreviousMenuItemSize.y, this.deltaPreviousMenuItemSize.z, menu[i].textureId, false);
                        }
                    }
                }
            }
        }

        private double Animate(double delta, double desired, double speed)
        {
            if (delta > desired) { return delta -= speed; } else if (delta < desired) { return delta += speed; } else return delta = desired;
        }

        private void AnimateVectorXYZ(VectorXYZ delta, VectorXYZ desired, double speedX, double speedY, double speedZ, double speedTheta)
        {
            delta.x = Animate(delta.x, desired.x, speedX);
            delta.y = Animate(delta.y, desired.y, speedY);
            delta.z = Animate(delta.z, desired.z, speedZ);
            delta.theta = Animate(delta.theta, desired.theta, speedTheta);
        }
        private void AnimateMove(VectorXYZ delta, VectorXYZ desired, double speed)
        {
            delta.x = Animate(delta.x, desired.x, speed);
            delta.y = Animate(delta.y, desired.y, speed);
            delta.z = Animate(delta.z, desired.z, speed*2.5);
            delta.theta = Animate(delta.theta, desired.theta, 1.5);
        }

        private void RenderField(bool lostGame)
        {
            AnimateMove(deltaMove, desiredMove, 2);
            GL.Translate(deltaMove.x, deltaMove.y + 20, deltaMove.z);
            GL.Rotate(deltaMove.theta, 1.0, 1.0, 0.0);

            for (int i = 0; i < gd.size.y; i++)
            {
                for (int j = 0; j < gd.size.x; j++)
                {
                    //cursor
                    if (gd.selected.x == j && gd.selected.y == i) gp.DrawCube((double)j * 20, (double)i * 20, 8.0, 5.0, 0.8, 0.0, 0.0);

                    if(cheats || lostGame)
                    {
                        if (gd.map[j, i].state == CellState.Hidden && !gd.map[j, i].isMine)
                        {
                            gp.DrawCube((double)j * 20, (double)i * 20, 0.0, 10.0, 0.9, 0.9, 0.9);
                        }
                        if (gd.map[j, i].isMine)
                        {
                            if(lostGame) { gp.DrawCube((double)j * 20, (double)i * 20, 0.0, 10.0, 0.9, 0.5, 0.1); }
                            else gp.DrawTexture((double)j * 20, (double)i * 20, 0.0, 10.0, 10); // draws mine
                        }
                    }
                    else
                    {
                        if (gd.map[j, i].state == CellState.Hidden)
                        {
                            gp.DrawCube((double)j * 20, (double)i * 20, 0.0, 10.0, 0.9, 0.9, 0.9);
                        }
                    }
                    
                    if (gd.map[j, i].state == CellState.Flagged)
                    {
                        gp.DrawTexture((double)j * 20, (double)i * 20, 0.0, 10.0, 19);
                    }

                    if (gd.map[j, i].state == CellState.QuestionMark)
                    {
                        gp.DrawTexture((double)j * 20, (double)i * 20, 0.0, 10.0, 20);
                    }

                    if (gd.map[j, i].state == CellState.Shown && gd.map[j, i].minesAround > 0)
                    {
                        gp.DrawNum(gd.map[j, i].minesAround, (double)j * 20, (double)i * 20, 0.0, 10.0, theta);
                    }
                }
            }
        }
    }
}
