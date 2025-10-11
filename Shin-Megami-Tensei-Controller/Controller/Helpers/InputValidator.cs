using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public static class InputValidator
{
    public static int ReadValidatedIndex(View view, int totalOptions)
    {
        string userInput = view.ReadLine();

        if (!IsNumericInput(userInput, out int selectedIndex))
            return GetInvalidIndex();

        int adjustedIndex = AdjustToZeroBased(selectedIndex);
        bool isWithinValidRange = IsWithinValidRange(adjustedIndex, totalOptions);

        return GetValidatedIndex(adjustedIndex, isWithinValidRange);
    }

    private static bool IsNumericInput(string input, out int number) => int.TryParse(input, out number);
    private static bool IsWithinValidRange(int index, int totalOptions) => index >= 0 && index < totalOptions;
    private static int AdjustToZeroBased(int index) => index - 1;
    private static int GetValidatedIndex(int index, bool isValid) => isValid ? index : GetInvalidIndex();
    private static int GetInvalidIndex() => -1;
}
