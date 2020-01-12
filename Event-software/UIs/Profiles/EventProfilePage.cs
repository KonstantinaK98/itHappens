﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using itHappens.Classes;
using itHappens.UIs.Common;
using itHappends;
using dbstuff;

namespace itHappens.UIs.andrea
{
    public partial class EventProfilePage : UserControl
    {
        public int organizerId { get; set; }
        public int eventId { get; set; }
        private DbController dbCon = new DbController();

        private static EventProfilePage _instance = new EventProfilePage();
        public static EventProfilePage Instance => _instance;

        public EventProfilePage()
        {
            InitializeComponent();
            if (Classes.DatabaseGeneralMethods.CheckIfEventBelongsToLoggedInUser(UIs.Common.EventMiniView.eventId))
            {
                EditButton.Visible = true;
            }
            else
            {
                EditButton.Visible = false;
            }
            friends();

        }


      

        public EventProfilePage(int organizerId, int eventId, string eventName, string venueName, string categoryColor, Image background, string organizerName, string organizerSurename, DateTime eventDateTime, double ticketPrice, string description)

        {
            InitializeComponent();
            if (Classes.DatabaseGeneralMethods.CheckIfEventBelongsToLoggedInUser(UIs.Common.EventMiniView.eventId))
            {
                EditButton.Visible = true;
            }
            else
            {
                EditButton.Visible = false;
            }


            friends();
            categoryColorPanel.BackColor = Utility.FromName(categoryColor);
            eventNameLabel.Text = eventName;
            monthLabel.Text = Utility.Month(eventDateTime);
            dayLabel.Text = Utility.Day(eventDateTime);
            backgroundPictureBox.BackgroundImage = background;
            locationTextBox.Text = venueName;
            organizerTextBox.Text = organizerName +" "+ organizerSurename ;
            dayTimeTextBox.Text = Utility.DateToText(eventDateTime);
            ticketPriceLabel.Text = ticketPrice + "";
            descriptionTextBox.Text = description;

            this.organizerId = organizerId;
            this.eventId = eventId;
        }

        private void friends()
        {
            for (int i = 0; i < 5; i++)
            {
                friendsFlowPanel.Controls.Add(new UIs.Common.FriendsAttending());
            }
        }

        public void friendsWhoWillAttend(Image friendProfilePicture, string friendName, string friendSurname, string attendingList)
        {
            friendsFlowPanel.Controls.Add(new UIs.Common.FriendsAttending(friendProfilePicture, friendName, friendSurname, attendingList));
        }

        private void EventProfilePage_Load(object sender, EventArgs e)
        {

        }

        private void descriptionLabel_Click(object sender, EventArgs e)
        {

        }

      /*  public static void openEventProfile(object sender, EventArgs e)
        {

            var eventview = (EventMiniView)sender;
            Controllers.UIController.Instance.MainSplitForm.middlePanel.Controls.Clear();
            int eventid = eventview.eventId;
            var v = Db_connector.Query(@"Select  event.title, venues.name, category.color, user.name ,
                        user.surname, event.startingDate,
						event.ticketprice, event.description, user.id , event.id
						FROM events e JOIN venues v
						ON e.venueID = v.id
						JOIN category c
						ON e.categoryID = c.id
						JOIN user u
						ON e.ownerID = u.id
						WHERE @eventid = e.id",
                       new string[,] { { "@eventid", eventid + "" } });
            v.Read();
            var middlePage = new EventProfilePage(
                v.GetInt32(8), v.GetInt32(9),
                v.GetString(0), v.GetString(1),
                 v.GetString(2), null, v.GetString(3),
                 v.GetString(4), v.GetDateTime(5),
                 v.GetDouble(6), v.GetString(7));
            Controllers.UIController.Instance.MainSplitForm.middlePanel.Controls.Add(middlePage);
            middlePage.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Left);
            middlePage.Dock = DockStyle.Fill;

        }*/

        private void organizerTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void locationTextBox_TextChanged(object sender, EventArgs e)
        {
            Controllers.UIController.Instance.searchToolStripMenuItem_MiddlePanel();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            Classes.MiddlePanelMethods.Instance.createEventToolStripMenuItem("edit");

        }

        private void interestedButton_Click(object sender, EventArgs e)
        {
            dbCon.addInterested(this.eventId, UIs.anna.LogInPage.userId);
        }

        private void goingButton_Click(object sender, EventArgs e)
        {
            dbCon.addGoing(this.eventId, UIs.anna.LogInPage.userId);
        }

        private void categoryColorPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void friendsAttendingPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
