﻿using System;
using Server;
using Server.Gumps;

namespace CustomsFramework
{
    public class BaseModule : BaseCore, ICustomsEntity, ISerializable
    {
        private Mobile _LinkedMobile;
        private Item _LinkedItem;
        private DateTime _CreatedTime;
        private DateTime _LastEditedTime;
        public BaseModule()
        {
        }

        public BaseModule(Mobile from)
        {
            LinkMobile(from);
        }

        public BaseModule(Item item)
        {
            LinkItem(item);
        }

        public BaseModule(CustomSerial serial)
            : base(serial)
        {
        }

        public override string Name
        {
            get
            {
                return @"Base Module";
            }
        }
        public override string Description
        {
            get
            {
                return "Base Module, inherit from this class and override all interface items.";
            }
        }
        public override string Version
        {
            get
            {
                return "1.0";
            }
        }
        public override AccessLevel EditLevel
        {
            get
            {
                return AccessLevel.Developer;
            }
        }
        public override Gump SettingsGump
        {
            get
            {
                return null;
            }
        }
        [CommandProperty(AccessLevel.Administrator)]
        public Mobile LinkedMobile
        {
            get
            {
                return this._LinkedMobile;
            }
            set
            {
                this._LinkedMobile = value;
            }
        }
        [CommandProperty(AccessLevel.Administrator)]
        public Item LinkedItem
        {
            get
            {
                return this._LinkedItem;
            }
            set
            {
                this._LinkedItem = value;
            }
        }
        [CommandProperty(AccessLevel.Administrator)]
        public DateTime CreatedTime
        {
            get
            {
                return this._CreatedTime;
            }
        }
        [CommandProperty(AccessLevel.Administrator)]
        public DateTime LastEditedTime
        {
            get
            {
                return this._LastEditedTime;
            }
        }
        public override string ToString()
        {
            return this.Name;
        }

        public override void Prep()
        {
        }

        public override void Delete()
        {
        }

        public virtual void Update()
        {
            this._LastEditedTime = DateTime.Now;
        }

        public virtual bool LinkMobile(Mobile from)
        {
            if (this._LinkedMobile != null)
                return false;
            else if (this._LinkedMobile == from)
                return false;
            else
            {
                this._LinkedMobile = from;
                this.Update();
                return true;
            }
        }

        public virtual bool LinkItem(Item item)
        {
            if (this._LinkedItem == null)
                return false;
            else if (this._LinkedItem == item)
                return false;
            else
            {
                this._LinkedItem = item;
                this.Update();
                return true;
            }
        }

        public virtual bool UnlinkMobile()
        {
            if (this._LinkedMobile == null)
                return false;
            else
            {
                this._LinkedMobile = null;
                this.Update();
                return true;
            }
        }

        public virtual bool UnlinkItem()
        {
            if (this._LinkedItem == null)
                return false;
            else
            {
                this._LinkedItem = null;
                this.Update();
                return true;
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            Utilities.WriteVersion(writer, 0);

            // Version 0
            writer.Write(this._LinkedMobile);
            writer.Write(this._LinkedItem);
            writer.Write(this._CreatedTime);
            writer.Write(this._LastEditedTime);
        }

        public override void Deserialize(GenericReader reader)
        {
            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        this._LinkedMobile = reader.ReadMobile();
                        this._LinkedItem = reader.ReadItem();
                        this._CreatedTime = reader.ReadDateTime();
                        this._LastEditedTime = reader.ReadDateTime();
                        break;
                    }
            }
        }
    }
}