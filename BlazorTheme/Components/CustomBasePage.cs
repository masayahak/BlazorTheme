﻿using BlazorTheme.Components.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorTheme.Components
{
    public class CustomBasePage : ComponentBase
    {
        [Inject]
        protected ThemeService ThemeService { get; set; } = default!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // StreamRenderingの場合、OnAfterRenderは複数回実行される。
            // 初回だけ実行するようにしてしまうと、<table>タグが生成されるまえにだけ
            // Applyする可能性があるので、冗長だがAfterRenderのたびに実行する。
            await ThemeService.GetCurrentTheme();
            await ThemeService.ApplyCurrentTheme();
        }
    }
}
