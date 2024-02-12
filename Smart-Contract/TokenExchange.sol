// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

import "@openzeppelin/contracts/token/ERC20/IERC20.sol";


contract TokenExchange {
    address public owner;
    address public obsToken;
    
    mapping(address => bool) public supportedTokens;
    mapping(address => uint256) public exchangeRates;

    event Exchange(
        address indexed buyer,
        address indexed token,
        uint256 ethAmount,
        uint256 obsAmount
    );

    modifier onlyOwner() {
        require(msg.sender == owner, "Not the owner");
        _;
    }

    constructor(address _obsTokenAddress) {
        owner = payable(msg.sender);
        obsToken = _obsTokenAddress;
    }

    function calculateEquivalentOBSAmount(address tokenAddress, uint256 paymentAmount, uint256 tokenDecimal) external view returns (uint256) {
        require(supportedTokens[tokenAddress], "Token not supported");
        return paymentAmount * exchangeRates[tokenAddress];
    }
function sendViaCall(address payable _to) public payable {
        // Call returns a boolean value indicating success or failure.
        // This is the current recommended method to use.
        (bool sent, bytes memory data) = _to.call{value: msg.value}("");
        require(sent, "Failed to send Ether");
    }
    function exchangeTokensForOBSTokens(address tokenAddress, uint256 tokenAmount, bool isToken, uint256 tokenDecimal) external payable {
    require(supportedTokens[tokenAddress], "Token not supported");

    if (isToken) {
        // Transfer the accepted token amount to the owner
        IERC20 token = IERC20(tokenAddress);
        token.transferFrom(msg.sender, owner, tokenAmount);
    } else {
        require(msg.value > 0, "Insufficient ETH sent");
        sendViaCall(payable(owner)); // Send ETH to the owner
    }

    uint256 obsDecimal = 16;
    
    // Calculate the adjustment factor
    uint256 adjustmentFactor = 10**(obsDecimal - tokenDecimal);
    
    // Calculate the rate
    uint256 rate = (exchangeRates[tokenAddress] * (10**16)) * adjustmentFactor;
    
    // Convert ETH to OBS in wei
    uint256 obsAmount = (tokenAmount * rate) / (10**16);
    // Transfer the equivalent OBS amount to the sender
    IERC20 obsTokenContract = IERC20(obsToken);
    obsTokenContract.transferFrom(owner, msg.sender, obsAmount);
        emit Exchange(msg.sender, tokenAddress, isToken ? tokenAmount : msg.value, obsAmount);
}



    function modifySupportedTokens(address tokenAddress, bool isActive, uint256 rate) external onlyOwner {
        supportedTokens[tokenAddress] = isActive;
        exchangeRates[tokenAddress] = rate;
    }

    function withdrawEth() external onlyOwner {
        payable(owner).transfer(address(this).balance);
    }

    function withdrawTokens(address tokenAddress) external onlyOwner {
        IERC20 token = IERC20(tokenAddress);
        uint256 balance = token.balanceOf(address(this));
        token.transfer(owner, balance);
    }
}
