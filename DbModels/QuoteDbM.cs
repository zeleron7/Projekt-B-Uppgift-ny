﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Seido.Utilities.SeedGenerator;
using Models;
using Models.DTO;

namespace DbModels;

[Table("Quotes", Schema = "supusr")]
sealed public class QuoteDbM : Quote, ISeed<QuoteDbM>, IEquatable<QuoteDbM>
{
    [Key]
    public override Guid QuoteId { get; set; }

    #region correcting the Navigation properties migration error caused by using interfaces
    [NotMapped]
    public override List<IFriend> Friends { get => FriendsDbM?.ToList<IFriend>(); set => new NotImplementedException(); }

    [JsonIgnore]
    public List<FriendDbM> FriendsDbM { get; set; } = null;
    #endregion

    #region constructors
    public QuoteDbM() : base() { }
    public QuoteDbM(SeededQuote goodQuote) : base(goodQuote) { }
    public QuoteDbM(QuoteCUdto org)
    {
        QuoteId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
    #endregion

    #region implementing IEquatable
    public bool Equals(QuoteDbM other) => (other != null) && ((QuoteText, Author) == (other.QuoteText, other.Author));
    public override bool Equals(object obj) => Equals(obj as QuoteDbM);
    public override int GetHashCode() => (QuoteText, Author).GetHashCode();
    #endregion

    #region randomly seed this instance
    public override QuoteDbM Seed(SeedGenerator sgen)
    {
        base.Seed(sgen);
        return this;
    }
    #endregion

    #region Update from DTO
    public Quote UpdateFromDTO(QuoteCUdto org)
    {
        if (org == null) return null;

        Author = org.Author;
        QuoteText = org.Quote;

        return this;
    }
    #endregion
}


