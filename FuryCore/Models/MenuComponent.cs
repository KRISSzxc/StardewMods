﻿namespace FuryCore.Models;

using System;
using FuryCore.Enums;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.Menus;

/// <summary>
/// </summary>
public class MenuComponent
{
    private readonly ClickableTextureComponent _component;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MenuComponent" /> class.
    /// </summary>
    /// <param name="component"></param>
    public MenuComponent(ClickableTextureComponent component, ComponentArea area = ComponentArea.Custom)
    {
        this.Area = area;
        this._component = component;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="menu"></param>
    /// <param name="componentType"></param>
    public MenuComponent(ItemGrabMenu menu, ComponentType componentType)
    {
        this.Menu = menu;
        this.ComponentType = componentType;
        this.Area = this.ComponentType switch
        {
            ComponentType.OrganizeButton => ComponentArea.Right,
            ComponentType.FillStacksButton => ComponentArea.Right,
            ComponentType.ColorPickerToggleButton => ComponentArea.Right,
            ComponentType.SpecialButton => ComponentArea.Right,
            ComponentType.JunimoNoteIcon => ComponentArea.Right,
            _ => ComponentArea.Custom,
        };
    }

    /// <summary>
    /// 
    /// </summary>
    public ComponentArea Area { get; }

    /// <summary>
    /// 
    /// </summary>
    public ComponentType ComponentType { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public ClickableTextureComponent Component
    {
        get
        {
            return this._component ?? this.ComponentType switch
            {
                ComponentType.OrganizeButton => this.Menu?.organizeButton,
                ComponentType.FillStacksButton => this.Menu?.fillStacksButton,
                ComponentType.ColorPickerToggleButton => this.Menu?.colorPickerToggleButton,
                ComponentType.SpecialButton => this.Menu?.specialButton,
                ComponentType.JunimoNoteIcon => this.Menu?.junimoNoteIcon,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public int Id
    {
        get
        {
            if (this.Component?.myID == -500)
            {
                this.Component.myID = MenuComponent.ComponentId++;
            }

            return this.Component?.myID ?? 0;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual string HoverText
    {
        get => this.Component?.hoverText;
    }

    /// <summary>
    /// 
    /// </summary>
    public bool IsCustom
    {
        get => this._component is not null;
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual string Name
    {
        get => this.Component.name;
        set => this.Component.name = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual int X
    {
        get => this.Component.bounds.X;
        set => this.Component.bounds.X = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual int Y
    {
        get => this.Component.bounds.Y;
        set => this.Component.bounds.Y = value;
    }

    private static int ComponentId { get; set; } = 69420;

    private ItemGrabMenu Menu { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="spriteBatch"></param>
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        this.Component?.draw(spriteBatch);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="maxScaleIncrease"></param>
    public virtual void TryHover(int x, int y, float maxScaleIncrease = 0.1f)
    {
        this.Component?.tryHover(x, y, maxScaleIncrease);
    }
}