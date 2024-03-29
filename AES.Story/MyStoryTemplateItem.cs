﻿using AES.Domain;

namespace AES.Story;

public abstract class MyStoryTemplateItem : DomainObject
{
    public int ItemIndex { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;

    public abstract StoryItem CreateStoryItem();
}