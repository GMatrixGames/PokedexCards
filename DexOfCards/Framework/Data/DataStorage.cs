﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DexOfCards.Framework.Data.Models;
using DexOfCards.Framework.Storage;
using DexOfCards.Utilities;

namespace DexOfCards.Framework.Data;

public static class DataStorage
{
    public static List<CardModel> Cards = new();
    public static List<CardSetModel> CardSets = new();

    public static void Init()
    {
        InitCards();
        InitSets();
    }

    public static async void InitCards()
    {
        Cards.Clear();
        await using var conn = SQLite.GetSql();
        var read = await conn.ExecuteReaderAsync("SELECT * FROM cards");
        while (await read.ReadAsync())
        {
            Cards.Add(new CardModel(
                read.GetString("cardName"),
                read.GetString("cardSet"),
                read.GetString("cardNumber"),
                read.GetString("cardImage"),
                read.GetString("language")
            ));
        }

        Cards.Sort((a, b) => string.Compare(a.CardSet, b.CardSet, StringComparison.Ordinal));
    }

    public static async void InitSets()
    {
        CardSets.Clear();
        await using var conn = SQLite.GetSql();
        var read = await conn.ExecuteReaderAsync("SELECT * FROM sets");
        while (await read.ReadAsync())
        {
            CardSets.Add(new CardSetModel(
                read.GetString("setid"),
                read.GetString("setname"),
                read.GetString("cardsInSet"),
                read.GetString("setImage"),
                read.GetString("language")
            ));
        }

        CardSets.Sort((a, b) => string.Compare(a.SetId, b.SetId, StringComparison.Ordinal));
    }

    public static CardSetModel GetSet(CardModel model) => CardSets.FirstOrDefault(a => a.SetId == model.CardSet);
    public static CardSetModel GetSet(string model) => CardSets.FirstOrDefault(a => a.SetId == model);
    public static List<CardModel> GetCards(CardSetModel model) => Cards.Where(a => a.CardSet == model.SetId).OrderBy(a => a.CardNumber).ToList();
}