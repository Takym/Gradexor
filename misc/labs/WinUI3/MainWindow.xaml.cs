using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace WinUI3
{
	partial class MainWindow
	{
		private readonly MicaBackdrop           _mica_inst;
		private readonly MicaBackdrop           _mica_inst_a;
		private readonly DesktopAcrylicBackdrop _desk_inst;
		private readonly CustomBackdrop         _cust_inst;
		private          TextBlock?             _msg;
		private          int                    _count;

		public MainWindow()
		{
			this.InitializeComponent();

			switch (this.SystemBackdrop) {
			case MicaBackdrop micaInst when micaInst.Kind == MicaKind.Base:
				_mica_inst            = micaInst;
				_mica_inst_a          = new() { Kind = MicaKind.BaseAlt };
				_desk_inst            = new();
				_cust_inst            = new();
				backdrop.SelectedItem = mica;
				break;
			case MicaBackdrop micaInst when micaInst.Kind == MicaKind.BaseAlt:
				_mica_inst            = new() { Kind = MicaKind.Base };
				_mica_inst_a          = micaInst;
				_desk_inst            = new();
				_cust_inst            = new();
				backdrop.SelectedItem = micaa;
				break;
			case DesktopAcrylicBackdrop deskInst:
				_mica_inst            = new() { Kind = MicaKind.Base    };
				_mica_inst_a          = new() { Kind = MicaKind.BaseAlt };
				_desk_inst            = deskInst;
				_cust_inst            = new();
				backdrop.SelectedItem = desktopAcrylic;
				break;
			case CustomBackdrop custInst:
				_mica_inst            = new() { Kind = MicaKind.Base    };
				_mica_inst_a          = new() { Kind = MicaKind.BaseAlt };
				_desk_inst            = new();
				_cust_inst            = custInst;
				backdrop.SelectedItem = custom;
				break;
			default:
				_mica_inst            = new() { Kind = MicaKind.Base    };
				_mica_inst_a          = new() { Kind = MicaKind.BaseAlt };
				_desk_inst            = new();
				_cust_inst            = new();
				backdrop.SelectedItem = none;
				break;
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (_msg is null) {
				panel.Children.Add(_msg = new TextBlock() {
					Text = "ボタンがクリックされました。"
				});
				_count = 1;
			} else {
				++_count;
				this.UpdateMessage();
			}
		}

		private void displayMastery_CheckedUnchecked(object sender, RoutedEventArgs e)
		{
			if (_count < 2) {
				return;
			}
			this.UpdateMessage();
		}

		private void UpdateMessage()
		{
			_msg?.Text = $"ボタンが{_count}回クリックされました。{(
				(displayMastery.IsChecked ?? false)
					? _count switch {
						>= 50 => "貴方はボタンクリックの神です。",
						>= 40 => "貴方はボタンクリックの帝王です。",
						>= 30 => "貴方はボタンクリックの賢者です。",
						>= 20 => "貴方はボタンクリックの熟練者です。",
						>= 15 => "貴方はボタンクリックの就業者です。",
						>=  5 => "貴方はボタンクリックの見習いです。",
						_     => string.Empty
					}
					: string.Empty
			)}";
		}

		private void backdrop_SelectionChanged(object sender, SelectionChangedEventArgs e)
			=> this.SystemBackdrop =
				ReferenceEquals(backdrop.SelectedItem, mica          ) ? _mica_inst   :
				ReferenceEquals(backdrop.SelectedItem, micaa         ) ? _mica_inst_a :
				ReferenceEquals(backdrop.SelectedItem, desktopAcrylic) ? _desk_inst   :
				ReferenceEquals(backdrop.SelectedItem, custom        ) ? _cust_inst   :
				null;
	}
}
